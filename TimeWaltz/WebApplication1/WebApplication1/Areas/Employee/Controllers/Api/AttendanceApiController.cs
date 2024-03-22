using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.PersonalRecordViewModels;
using WebApplication1.Services;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using RequestStatusEnum = Repository.Enum.RequestStatusEnum;


namespace WebApplication1.Areas.Employee.Controllers.Api
{
    [Route("mobile/api/Attendance/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    public class AttendanceApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;
        private readonly CompRequestService _service;
        private readonly RequestStatusService _requestService;
        private readonly ApprovalService _approvalService;

        public AttendanceApiController(TimeWaltzContext db,CompRequestService service, RequestStatusService requestService , ApprovalService approvalService)
        {
            _db = db;
            _service = service;
            _requestService = requestService;
            _approvalService = approvalService;
        }


        [HttpGet]
        public object All()
        {
            var flex = _db.Flextimes.First();
            var shifts = GetEmpShifts(User.GetId());
            var approvalIds = _db.Approvals.AsNoTracking().Where(x => x.Status == RequestStatusEnum.簽核完成 && x.TableType == (int)TableTypeEnum.請假單).Select(x => x.TableId).ToList();
            var leave = _db.LeaveRequests.AsNoTracking().Where(x => x.EmployeesId == User.GetId() && x.StartTime <= DateTime.Now.Date && approvalIds.Contains(x.Id)).ToList();

            var clocks = _db.Clocks.Where(x => x.EmployeesId == User.GetId()).GroupBy(x => x.Date.Date)
                .ToDictionary(k => k.Key, v => new
                {
                    On = v.Where(x => x.Status == ClockStatusEnum.上班打卡).MinBy(x => x.Date),
                    Off = v.Where(x => x.Status == ClockStatusEnum.下班打卡).MaxBy(x => x.Date)
                });


            var result = shifts.AsParallel().Select(x =>
            {
                var scheduleStart = x.ShiftsDate.Date + x.ShiftSchedule.StartTime.TimeOfDay;
                var scheduleEnd = x.ShiftsDate.Date + x.ShiftSchedule.EndTime.TimeOfDay;
                if (!clocks.TryGetValue(x.ShiftsDate.Date, out var work)) return new
                {
                    start = scheduleStart.ToString("u").Replace(" ", "T"),
                    end = scheduleEnd.ToString("u").Replace(" ", "T"),
                    title = "打卡異常(尚未打卡)"
                };
                var result = CalculateWorkStatus(scheduleStart, scheduleEnd, work.On?.Date, work.Off?.Date, flex.MoveUp.GetValueOrDefault(), flex.FlexibleTime);
                return new
                {
                    start = work.On == null ? "" : work.On.Date.ToString("u").Replace(" ", "T"),
                end = work.Off == null ? "" : work.Off.Date.ToString("u").Replace(" ", "T"),
                    title = result
                };

            });
            //todo 計算請假時段 移除打卡紀錄
            return result.OrderBy(x=>x.start);

        }

        [NonAction]
        private List<Shift> GetEmpShifts(int userId)
        {
            var shifts = _db.Shifts.AsNoTracking().Include(x => x.ShiftSchedule)
                .Where(x => x.EmployeesId == userId && x.ShiftsDate.Date <= DateTime.Now.Date).ToList();
            return shifts;
        }
        [NonAction]
        private static string CalculateWorkStatus(DateTime scheduleStart, DateTime scheduleEnd, DateTime? workOn, DateTime? workOff, bool canEarly, int mins)
        {
            // 檢查是否有打卡記錄
            if (!workOn.HasValue || !workOff.HasValue) return "無完整打卡紀錄";

            // 計算彈性上班的可能的最早和最晚打卡時間
            var earliestStart = canEarly ? scheduleStart.AddMinutes(-mins) : scheduleStart;
            var latestStart = scheduleStart.AddMinutes(mins);
            var earliestEnd = scheduleEnd.AddMinutes(-mins);
            var latestEnd = scheduleEnd.AddMinutes(mins);

            // 檢查上班打卡是否遲到
            if (workOn > latestStart) return "遲到";

            // 檢查下班打卡是否早退
            if (workOff < earliestEnd) return "早退";

            // 檢查工作時長是否足夠
            return (workOff.Value - workOn.Value).TotalHours < 9 ? "時數不夠" : "正常";
        }
        [HttpPost]
        public bool CompRequestCreate(CompRequestCreateViewModel model)
        {
            try
            {
                var approvalEmp = _service.GetApprovalEmp(model.EmployeesId);
                model.ApprovalEmployeeId = approvalEmp;

                var entity = new AdditionalClockIn
                {
                    EmployeesId = model.EmployeesId,
                    ApprovalEmployeeId = model.ApprovalEmployeeId,
                    AdditionalTime = model.AdditionalTime,
                    Status = ((int)model.Status),
                    Reason = model.Reason,
                };
                _service.CreateCompRequest(entity);

                
                _approvalService.NewApproval_補打卡(entity.Id);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public ActionResult<CompRequestDto> List()
        {
            try
            {
                var empId = User.GetId();
                var models = _service.GetCompRequesListData(empId);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CompRequestDetailViewModel> GetCompRequestData(int Id)
        {
            try
            {
                var dto = _service.GetEditDataOrNull(Id);
                return Ok(dto);
            }
            catch
            {
                return Ok(new { status = false });
            }
        }

    }
}
