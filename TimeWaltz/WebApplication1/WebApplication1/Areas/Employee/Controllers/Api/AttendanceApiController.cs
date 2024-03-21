using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Helpers;
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

        public AttendanceApiController(TimeWaltzContext db)
        {
            _db = db;
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
                var title = "";
                var start = x.ShiftsDate.Date + x.ShiftSchedule.StartTime.TimeOfDay;
                var end = x.ShiftsDate.Date + x.ShiftSchedule.StartTime.TimeOfDay;
                if (clocks.TryGetValue(x.ShiftsDate.Date, out var work))
                {
                    if (work.On == null || work.Off == null)
                    {
                        title = "打卡異常(尚未打卡)";
                    }
                    else
                    {
                        if ((work.Off.Date - work.On.Date).TotalHours < 8)
                        {
                            title = "打卡異常(不滿8小時)";
                            start = work.On.Date;
                            end = work.Off.Date;
                        }
                        else
                        {
                            title = "正常上班";
                            start = work.On.Date;
                            end = work.Off.Date;
                        }
                    }
                }
                else
                {
                    title = "打卡異常(尚未打卡)";
                }

                return new
                {
                    start = start,
                    end = end,
                    title = title
                };
            });

            return result.OrderBy(x=>x.start);

        }

        [NonAction]
        private List<Shift> GetEmpShifts(int userId)
        {
            var shifts = _db.Shifts.AsNoTracking().Include(x => x.ShiftSchedule)
                .Where(x => x.EmployeesId == userId && x.ShiftsDate.Date <= DateTime.Now.Date).ToList();
            return shifts;
        }
    }
}
