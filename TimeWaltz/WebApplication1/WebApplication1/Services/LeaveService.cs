using Humanizer;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.PersonalRecordViewModels;

namespace WebApplication1.Services
{
    public class LeaveService
    {
        private readonly TimeWaltzContext _timeWaltzContext;
        private readonly IWebHostEnvironment _env;

        public LeaveService(TimeWaltzContext timeWaltzContext, IWebHostEnvironment webHostEnvironment)
        {
            this._timeWaltzContext = timeWaltzContext;
            this._env = webHostEnvironment;
        }
        public List<Employee> GetAgentDropDownData(int userId)
        {
            var employee = _timeWaltzContext.Employees.ToList();
            var agentEmployee = _timeWaltzContext.Employees.Include(x=>x.AgentEmployees).FirstOrDefault(x => x.Id == userId).AgentEmployees;
            return agentEmployee.ToList();
        }

        /// <summary>
        /// 取得假別下拉選單
        /// </summary>
        /// <returns></returns>

        internal string GetApprovalStatusOrDefault()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 新增一筆LeaveRequest
        /// </summary>
        /// <param name="entity"></param>
        public void CreateLeaveRequest(LeaveRequest entity)
        {
            _timeWaltzContext.LeaveRequests.Add(entity);
            _timeWaltzContext.SaveChanges();
        }
        /// <summary>
        /// 處裡上傳檔案
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetRelativeFileRoute(IFormFile file)
        {
            var relativePath = "";
            if (file!= null)
            {
                var dir = $"{_env.WebRootPath}";
                relativePath = $"/pic/{Guid.NewGuid()}_{file.FileName}";


                using (var fs = new FileStream(dir + relativePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                
                return relativePath;
            }
            return "";

        }
        /// <summary>
        /// 取得請假歷史紀錄清單資料
        /// </summary>
        /// <returns></returns>
        public List<LeaveDto> GetLeaveListData(int empId)
        {

            return _timeWaltzContext.LeaveRequests
                .Where(x=>x.EmployeesId == empId)
                .Join(_timeWaltzContext.Approvals, x => x.Id, y => y.TableId, (x, y) => new { x, y })
                .Where(xy => xy.y.TableType == (int)TableTypeEnum.請假單)
                .Select(xy => new LeaveDto
            {
                Id = xy.x.Id,
                EmployeesId = xy.x.EmployeesId,
                VacationType = xy.x.VacationDetails.VacationType,
                AgentEmployeeName = xy.x.AgentEmployee.Name,
                ApprovalEmpName = xy.x.AgentEmployee.Name,
                StartTime = xy.x.StartTime.ToString("yyyy-MM-dd HH:mm"),
                EndTime = xy.x.EndTime.ToString("yyyy-MM-dd HH:mm"),
                ApprovalStatus = xy.y.Status.ToString(),
            }).ToList();
        }
        /// <summary>
        /// 取得簽核人資料
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public int GetApprovalEmp(int empId)
        {
            return _timeWaltzContext.Employees.Include(x=>x.Department).FirstOrDefault(x=>x.Id == empId).Department.EmployeeId;
        }
        /// <summary>
        /// 取得請假詳細資料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public LeaveEditDto? GetEditDataOrNull(int Id)
        {
            var leaveRequest = _timeWaltzContext.LeaveRequests.FirstOrDefault(x=>x.Id == Id);
            if (leaveRequest == null) throw new NullReferenceException("Not find this user");


            var approval = _timeWaltzContext.Approvals.Where(x=>(int)x.TableType == 1).FirstOrDefault(x =>x.TableId == Id);
            if (approval == null) throw new NullReferenceException("Not find this user");

            var requestStatus = _timeWaltzContext.RequestStatuses.Where(x => (int)x.TableType == 1).FirstOrDefault(x => x.TableId == Id);
            if (requestStatus == null) throw new NullReferenceException("Not find this user");

            return new LeaveEditDto
            {
                Id = leaveRequest.Id,
                TimeRange = leaveRequest.StartTime.ToString("yyyy-MM-dd HH:mm") + "-" + leaveRequest.EndTime.ToString("yyyy-MM-dd HH:mm"),
                Reason = leaveRequest.Reason,
                EmployeeName = leaveRequest.Employees.Name,
                AgentEmployeeName = leaveRequest.AgentEmployee.Name,
                ApprovalEmpName = leaveRequest.ApprovalEmployee.Name,
                VacationType = leaveRequest.VacationDetails.VacationType,
                ApprovalRemark = approval.Remark,
                ApprovalStatus = requestStatus.Status.ToString(),                
            };
        }

        #region New Logic
        private static TimeSpan 計算總共請假時間(DateTime leaveStart, DateTime leaveEnd, IEnumerable<WorkDays> workDays)
        {
            TimeSpan totalLeaveHours = TimeSpan.Zero;
            foreach (var workDay in workDays)
            {
                if (!IsOverlap(leaveStart, leaveEnd, workDay.Start, workDay.End)) continue;

                var breakTime = GetTodayBreakTime(workDay.Start.Date);

                var start = MaxTime(leaveStart, workDay.Start);
                var end = MinTime(leaveEnd, workDay.End);

                if (IsOverlap(start, end, breakTime.start, breakTime.end))
                {
                    if (start < breakTime.start)
                        totalLeaveHours += (breakTime.start - start);

                    if (end > breakTime.end)
                        totalLeaveHours += (end - breakTime.end);
                }
                else
                {
                    totalLeaveHours += (end - start);
                }
            }
            return totalLeaveHours;
        }
        private static DateTime MaxTime(DateTime dateTime1, DateTime dateTime2) => dateTime1 > dateTime2 ? dateTime1 : dateTime2;
        private static DateTime MinTime(DateTime dateTime1, DateTime dateTime2) => dateTime1 < dateTime2 ? dateTime1 : dateTime2;
        private static bool IsOverlap(DateTime start, DateTime end, DateTime compareStart, DateTime compareEnd) => start < compareEnd && end > compareStart;
        private static (DateTime start, DateTime end) GetTodayBreakTime(DateTime today) => (today.AddHours(12), today.AddHours(13));

        #endregion
        public int AddLeaveTime2(LeaveCreateModel model)
        {
            var emp = _timeWaltzContext.Employees.Include(x => x.Shifts).ThenInclude(x => x.ShiftSchedule).FirstOrDefault(x => x.Id == model.EmployeesId);
            if (emp == null) throw new Exception("Not find this employee");


            var workDays = emp.Shifts.Where(x => x.ShiftsDate.Date >= model.StartTime.Date && x.ShiftsDate.Date <= model.EndTime.Date).Select(x=> new WorkDays
            {
                Start = x.ShiftsDate.Date + x.ShiftSchedule.StartTime.TimeOfDay,
                End = x.ShiftsDate.Date+ x.ShiftSchedule.EndTime.TimeOfDay,
            });

            var totalTimeSpan = 計算總共請假時間(model.StartTime,model.EndTime,workDays);

            return (int)totalTimeSpan.TotalMinutes;

        }

        public TimeSpan CalculateLeaveTime(DateTime startTime, DateTime endTime, IEnumerable<WorkDays> workDays)
        {
            var currentDay = startTime.Date;
            var totalDuration = TimeSpan.Zero;
            while (currentDay <= endTime.Date)
            {
                var workDay = workDays.FirstOrDefault(x => x.Start.Date == currentDay.Date);
                if (workDay != null)
                {
                    var currentLeaveStart = startTime > workDay.Start? startTime:workDay.Start;
                    var currentLeaveEnd = endTime < workDay.End ? endTime : workDay.End;

                    //break time
                    var breakTimeStart = workDay.Start.Date + TimeSpan.FromHours(12);
                    var breakTimeEnd = workDay.Start.Date + TimeSpan.FromHours(13);
                    if(currentLeaveStart < breakTimeEnd && currentLeaveEnd > breakTimeStart)
                    {
                        if (currentLeaveStart < breakTimeStart) currentLeaveStart = breakTimeEnd;
                        if (currentLeaveEnd > breakTimeEnd) currentLeaveEnd = currentLeaveEnd > breakTimeStart ? currentLeaveEnd - TimeSpan.FromHours(1) : currentLeaveEnd;
                    }
                    var total = currentLeaveEnd - currentLeaveStart;
                    totalDuration += total;
                }
                currentDay = currentDay.AddDays(1);
            }
            return totalDuration;
        }



    }
 
}

