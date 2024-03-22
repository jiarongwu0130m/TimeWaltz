using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Areas.Employee.Models;
using WebApplication1.Helpers;
using WebApplication1.Services;
using RequestStatusEnum = Repository.Enum.RequestStatusEnum;


namespace WebApplication1.Areas.Employee.Controllers.Api;

[Route("mobile/api/Attendance/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
public class AttendanceApiController : ControllerBase
{
    private readonly ApprovalRepository _approvalRepository;
    private readonly ApprovalService _approvalService;
    private readonly TimeWaltzContext _db;
    private readonly RequestStatusService _requestService;

    public AttendanceApiController(ApprovalRepository approvalRepository, TimeWaltzContext db,
        RequestStatusService requestService, ApprovalService approvalService)
    {
        _approvalRepository = approvalRepository;
        _db = db;
        _requestService = requestService;
        _approvalService = approvalService;
    }


    [HttpGet]
    public object All()
    {
        var flex = _db.Flextimes.First();
        var shifts = GetEmpShifts(User.GetId());
        var approvalIds = _approvalRepository.GetId(RequestStatusEnum.簽核完成, TableTypeEnum.請假單);
        var leave = _db.LeaveRequests.AsNoTracking().Where(x =>
            x.EmployeesId == User.GetId() && x.StartTime <= DateTime.Now.Date && approvalIds.Contains(x.Id)).ToList();

        var clocks = _db.Clocks.AsNoTracking().Where(x => x.EmployeesId == User.GetId()).GroupBy(x => x.Date.Date)
            .ToDictionary(k => k.Key, v => new
            {
                On = v.Where(x => x.Status == ClockStatusEnum.上班打卡).MinBy(x => x.Date),
                Off = v.Where(x => x.Status == ClockStatusEnum.下班打卡).MaxBy(x => x.Date)
            });

        var removedOverlapShifts =
            shifts.Where(x => !leave.Any(l => x.ShiftsDate >= l.StartTime && x.ShiftsDate <= l.EndTime));


        var result = removedOverlapShifts.AsParallel().Select(x =>
        {
            var scheduleStart = x.ShiftsDate.Date + x.ShiftSchedule.StartTime.TimeOfDay;
            var scheduleEnd = x.ShiftsDate.Date + x.ShiftSchedule.EndTime.TimeOfDay;


            if (!clocks.TryGetValue(x.ShiftsDate.Date, out var work))
                return new CalendarEventDto
                {
                    Start = scheduleStart.ToISODateTimeString(),
                    End = scheduleEnd.ToISODateTimeString(),
                    Title = "打卡異常(尚未打卡)"
                };
            var result = CalculateWorkStatus(scheduleStart, scheduleEnd, work.On?.Date, work.Off?.Date,
                flex.MoveUp.GetValueOrDefault(), flex.FlexibleTime);
            return new CalendarEventDto
            {
                Start = work.On == null ? "" : work.On.Date.ToISODateTimeString(),
                End = work.Off == null ? "" : work.Off.Date.ToISODateTimeString(),
                Title = result
            };
        });
        //todo 計算請假時段 移除打卡紀錄

        var leaveRange = leave.Select(x => new CalendarEventDto
        {
            Start = x.StartTime.ToISODateTimeString(),
            End = x.EndTime.ToISODateTimeString(),
            Title = "請假"
        });

        return leaveRange.Concat(result).Select((x, idx) => new CalendarEventDto
        {
            Id = idx,
            End = x.End,
            Start = x.Start,
            Title = x.Title
        });
    }

    [NonAction]
    private List<Shift> GetEmpShifts(int userId)
    {
        var shifts = _db.Shifts.AsNoTracking().Include(x => x.ShiftSchedule)
            .Where(x => x.EmployeesId == userId && x.ShiftsDate.Date <= DateTime.Now.Date).ToList();
        return shifts;
    }

    [NonAction]
    private static string CalculateWorkStatus(DateTime scheduleStart, DateTime scheduleEnd, DateTime? workOn,
        DateTime? workOff, bool canEarly, int mins)
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
}