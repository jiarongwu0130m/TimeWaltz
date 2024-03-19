using Microsoft.EntityFrameworkCore;
using Repository.Models;

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

        public List<VacationDetail> GetVacationDropDownData()
        {
            return _timeWaltzContext.VacationDetails.ToList();
        }
        internal string GetApprovalStatusOrDefault()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 取得請假當事人的姓名以及employeeId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Employee GetNameOrNull(int userId)
        {
            return _timeWaltzContext.Employees.Where(x => x.IsDelete == false).FirstOrDefault(x => x.Id == userId);
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
        /// <param name="model"></param>
        /// <returns></returns>
        public LeaveCreateDto GetRelativeFileRoute(LeaveCreateDto model)
        {
            var relativePath = "";
            if (model.FileRoute != null)
            {
                var dir = $"{_env.WebRootPath}";
                relativePath = $"/pic/{Guid.NewGuid()}_{model.FileRoute.FileName}";


                using (var fs = new FileStream(dir + relativePath, FileMode.Create))
                {
                    model.FileRoute.CopyTo(fs);
                }

                model.RelativeFileRoute = relativePath;
                return model;
            }
            return model;

        }
        /// <summary>
        /// 取得請假歷史紀錄清單資料
        /// </summary>
        /// <returns></returns>
        public List<LeaveRequest> GetLeaveListData()
        {
            var entities = _timeWaltzContext.LeaveRequests
                .Join(_timeWaltzContext.VacationDetails, l => l.VacationDetailsId, v => v.Id, (l, v) => new { l, v })
                .Join(_timeWaltzContext.Employees, lv => lv.l.EmployeesId, e => e.Id, (lv, e) => new { lv, e })
                .Join(_timeWaltzContext.Departments, lve => lve.e.Id, d => d.EmployeesId, (lve, d) => new { lve, d })
                .Join(_timeWaltzContext.Employees, lved => lved.lve.lv.l.AgentEmployeeId, a => a.Id, (lved, a) => new { lved, a })
                .Join(_timeWaltzContext.Employees, lveda => lveda.lved.lve.lv.l.ApprovalEmployeeId, ap => ap.Id, (lveda, ap) => new { lveda, ap })
                .Join(_timeWaltzContext.RequestStatuses, lvedaap => lvedaap.lveda.lved.lve.lv.l.Id, r => r.TableId, (lvedaap, r) => new { lvedaap, r })
                .Where(lvedaapr => (int)lvedaapr.r.TableType == 1).Select(lvedaapr => new LeaveRequest
                {
                    VacationType = lvedaapr.lvedaap.lveda.lved.lve.lv.v.VacationType,
                    AgentEmployeeName = lvedaapr.lvedaap.lveda.a.Name,
                    ApprovalStatus = lvedaapr.r.Status,
                    ApporvalEmpName = lvedaapr.lvedaap.ap.Name,
                    Id = lvedaapr.lvedaap.lveda.lved.lve.lv.l.Id,
                    StartTime = lvedaapr.lvedaap.lveda.lved.lve.lv.l.StartTime,
                    EndTime = lvedaapr.lvedaap.lveda.lved.lve.lv.l.EndTime,
                    EmployeeName = lvedaapr.lvedaap.lveda.lved.lve.e.Name,
                }).Distinct().ToList();
            return entities;
        }
        /// <summary>
        /// 取得簽核人資料
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        public LeaveCreateDto GetApprovalEmp(LeaveCreateDto fileModel)
        {
            var emp = _timeWaltzContext.Employees.FirstOrDefault(x => x.Id == fileModel.EmployeesId).DepartmentId;
            var sameEmp = _timeWaltzContext.Departments.FirstOrDefault(x => x.Id == emp).EmployeesId;
            if (sameEmp != null)
            {
                fileModel.ApprovalEmployeeId = (int)sameEmp;
            }
            return fileModel;
        }
        /// <summary>
        /// 取得請假詳細資料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public LeaveRequest? GetEditDataOrNull(int Id)
        {
            var leaveRequest = _timeWaltzContext.LeaveRequests.FirstOrDefault(x=>x.Id == Id);
            if (leaveRequest == null) throw new NullReferenceException("Not find this user");


            var approval = _timeWaltzContext.Approvals.Where(x=>(int)x.TableType == 1).FirstOrDefault(x =>x.TableId == Id);
            if (approval == null) throw new NullReferenceException("Not find this user");

            var requestStatus = _timeWaltzContext.RequestStatuses.Where(x => (int)x.TableType == 1).FirstOrDefault(x => x.TableId == Id);
            if (requestStatus == null) throw new NullReferenceException("Not find this user");

            return new LeaveRequest
            {
                Id = leaveRequest.Id,
                StartTime = leaveRequest.StartTime,
                EndTime = leaveRequest.EndTime,
                Reason = leaveRequest.Reason,
                EmployeeName = leaveRequest.Employees.Name,
                AgentEmployeeName = leaveRequest.AgentEmployee.Name,
                ApporvalEmpName = leaveRequest.ApprovalEmployee.Name,
                VacationType = leaveRequest.VacationDetails.VacationType,
                ApprovalRemark = approval.Remark,
                ApprovalStatus = requestStatus.Status,
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
        public LeaveCreateDto AddLeaveTime2(LeaveCreateDto model)
        {
            var emp = _timeWaltzContext.Employees.Include(x => x.Shifts).ThenInclude(x => x.ShiftSchedule).FirstOrDefault(x => x.Id == model.EmployeesId);
            if (emp == null) throw new Exception("Not find this employee");


            var workDays = emp.Shifts.Where(x => x.ShiftsDate.Date >= model.StartTime.Date && x.ShiftsDate.Date <= model.EndTime.Date).Select(x=> new WorkDays
            {
                Start = x.ShiftsDate.Date + x.ShiftSchedule.StartTime.TimeOfDay,
                End = x.ShiftsDate.Date+ x.ShiftSchedule.EndTime.TimeOfDay,
            });

            var totalTimeSpan = 計算總共請假時間(model.StartTime,model.EndTime,workDays);

            model.LeaveMinutes = (int)totalTimeSpan.TotalMinutes;
            return model;

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



        /// <summary>
        /// 計算一次假單的請假時數並存入資料庫
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public LeaveCreateDto AddLeaveTime(LeaveCreateDto model)
        {
            var leaveStart = model.StartTime;
            var leaveEnd = model.EndTime;

            var emp = _timeWaltzContext.Employees
                .Where(x=>x.Id == model.EmployeesId)
                .Join(_timeWaltzContext.ShiftSchedules, e => e.ShiftScheduleId, shiftSchedule => shiftSchedule.Id, (e, shiftSchedule) => new { e, shiftSchedule })
                .Join(_timeWaltzContext.Shifts, eshiftSchedule => eshiftSchedule.e.Id, shift=>shift.EmployeesId, (eshiftSchedule, shift)=> new { eshiftSchedule, shift})
                .ToList();
            if (emp == null) throw new Exception("程式錯誤");

            //請假時間是否多於一天的判斷
            if (!IsSameDay(leaveStart, leaveEnd))
            {
                //請假開始時間及結束時間在不同天
                TimeSpan startTimeOfDay, endTimeOfDay;
                var fullDays = CalculateFullDays(leaveStart, leaveEnd, out startTimeOfDay, out endTimeOfDay);

                for (var i = 0; i < 5; i++)
                {
                    leaveStart.AddDays(i);
                    var Have = emp.FirstOrDefault(x => x.shift.ShiftsDate.Date == leaveStart.Date);
                    if (Have != null)
                    {
                        model.LeaveMinutes = model.LeaveMinutes + 8;
                    }
                }
                var ShiftB = emp.FirstOrDefault(x => x.shift.ShiftsDate.Date == model.StartTime.Date);
                if (ShiftB != null)
                {
                    var shiftStart = ShiftB.eshiftSchedule.shiftSchedule.StartTime;
                    var shiftEnd = ShiftB.eshiftSchedule.shiftSchedule.EndTime;
                    //確認請假開始時間即結束時間在同一天
                    //判斷請假時間與班別時間的關係取得符合班別上下班時間的請假時間
                    var leaveStartTime = GetCorrectStartTime(leaveStart.TimeOfDay, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                    var leaveEndTime = GetCorrectEndTime(leaveStartTime, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                    //算出扣除掉午休時間的請假時間
                    var leaveTime = GetLeaveTimeSkipBreakTime(leaveStartTime, leaveEndTime);

                    model.LeaveMinutes = leaveTime / 60;
                    return model;
                }
                var ShiftC = emp.FirstOrDefault(x => x.shift.ShiftsDate.Date == model.StartTime.Date);
                if (ShiftC != null)
                {
                    var shiftStart = ShiftC.eshiftSchedule.shiftSchedule.StartTime;
                    var shiftEnd = ShiftC.eshiftSchedule.shiftSchedule.EndTime;
                    //確認請假開始時間即結束時間在同一天
                    //判斷請假時間與班別時間的關係取得符合班別上下班時間的請假時間
                    var leaveStartTime = GetCorrectStartTime(leaveStart.TimeOfDay, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                    var leaveEndTime = GetCorrectEndTime(leaveStartTime, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                    //算出扣除掉午休時間的請假時間
                    var leaveTime = GetLeaveTimeSkipBreakTime(leaveStartTime, leaveEndTime);

                    model.LeaveMinutes = leaveTime;
                    return model;
                }
            }
            else
            {
                //判斷請假單日期在班表上是否有班
                var ShiftA = emp.FirstOrDefault(x => x.shift.ShiftsDate.Date == model.StartTime.Date);
                if (ShiftA == null)
                {
                    model.LeaveMinutes = 0;
                    return model;
                }
                var shiftStart = ShiftA.eshiftSchedule.shiftSchedule.StartTime;
                var shiftEnd = ShiftA.eshiftSchedule.shiftSchedule.EndTime;
                //確認請假開始時間即結束時間在同一天
                //判斷請假時間與班別時間的關係取得符合班別上下班時間的請假時間
                var leaveStartTime = GetCorrectStartTime(leaveStart.TimeOfDay, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                var leaveEndTime = GetCorrectEndTime(leaveStartTime, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                //算出扣除掉午休時間的請假時間
                var leaveTime = GetLeaveTimeSkipBreakTime(leaveStartTime, leaveEndTime);

                model.LeaveMinutes = leaveTime / 60;
                return model;
            }

            throw new NotImplementedException("程式設計錯誤");



        }
        /// <summary>
        /// 判斷請假時間是否多於一天
        /// </summary>
        /// <param name="leaveStart"></param>
        /// <param name="leaveEnd"></param>
        /// <returns></returns>
        public bool IsSameDay(DateTime leaveStart, DateTime leaveEnd)
        {
            return leaveStart.Date == leaveEnd.Date;
        }
        /// <summary>
        /// 計算請假開始時間及結束時間中有幾個完整的一天
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="startTimeOfDay"></param>
        /// <param name="endTimeOfDay"></param>
        /// <returns></returns>
        static int CalculateFullDays(DateTime startTime, DateTime endTime, out TimeSpan startTimeOfDay, out TimeSpan endTimeOfDay)
        {
            // 設置開始日期的開始時間為當天的開始
            startTime = startTime.Date;
            startTimeOfDay = startTime.TimeOfDay;

            // 設置結束日期的結束時間為當天的結束
            endTime = endTime.Date.AddDays(1).AddSeconds(-1);
            endTimeOfDay = endTime.TimeOfDay;

            // 計算完整的天數
            int fullDays = (int)(endTime - startTime).TotalDays;

            // 確保完整的天數不為負數
            return Math.Max(0, fullDays);
        }
        /// <summary>
        /// 取得符合班表的正確的請假開始時間
        /// </summary>
        /// <param name="leaveStart"></param>
        /// <param name="leaveEnd"></param>
        /// <param name="shiftStart"></param>
        /// <param name="shiftEnd"></param>
        /// <returns></returns>

        public TimeSpan GetCorrectStartTime(TimeSpan leaveStart,TimeSpan leaveEnd, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            if (leaveStart <= shiftStart && leaveEnd <= shiftEnd)
            {               
                leaveStart = shiftStart;
                return leaveStart;
            }
            else if (leaveStart <= shiftStart && leaveEnd >= shiftEnd)
            {
                leaveStart = shiftStart;
                return leaveStart;
            }
            else
            {
                leaveStart = leaveEnd;
                return leaveStart;
            }
        }
        /// <summary>
        /// 取得符合班表的請假結束時間
        /// </summary>
        /// <param name="leaveStart"></param>
        /// <param name="leaveEnd"></param>
        /// <param name="shiftStart"></param>
        /// <param name="shiftEnd"></param>
        /// <returns></returns>
        public TimeSpan GetCorrectEndTime(TimeSpan leaveStart, TimeSpan leaveEnd, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            if (leaveEnd >= shiftEnd && leaveStart >= shiftStart)
            {
                leaveEnd = shiftEnd;
                return leaveEnd;
            }
            else if (leaveStart <= shiftStart && leaveEnd >= shiftEnd)
            {
                leaveEnd = shiftEnd;
                return leaveEnd;
            }
            else
            {
                leaveEnd = leaveStart;
                return leaveEnd;
            }
        }
        /// <summary>
        /// 取得扣除午休時間(12:00~13:00)的請假時間
        /// </summary>
        /// <param name="leaveStartTime"></param>
        /// <param name="leaveEndTime"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int GetLeaveTimeSkipBreakTime(TimeSpan leaveStartTime, TimeSpan leaveEndTime)
        {
            //一天之中請假時間與午休的關係
            TimeSpan ss = new DateTime(2024, 3, 17, 12, 0, 0).TimeOfDay;
            TimeSpan ee = new DateTime(2024, 3, 17, 13, 0, 0).TimeOfDay;

            if(leaveStartTime < ss && leaveEndTime <= ee && leaveEndTime > ss)
            {
                var data = (ss - leaveStartTime);
                return data.Minutes + data.Hours * 60;
            }
            else if(leaveStartTime < ss && leaveEndTime <= ss || leaveEndTime > ee && leaveStartTime >= ee)
            {
                var data = (leaveEndTime - leaveStartTime);
                return data.Minutes + data.Hours * 60;
            }
            else if(leaveStartTime < ss && leaveEndTime > ee)
            {
                var data1 = (ss - leaveStartTime);
                var data2 = (leaveEndTime - ee);
                return data1.Minutes + data1.Hours * 60 + data2.Minutes + data2.Hours * 60;
            }
            else if(leaveEndTime > ee && leaveStartTime < ee && leaveStartTime >= ss)
            {
                var data = (leaveEndTime - ee);
                return data.Minutes + data.Hours * 60;
            }
            else if(leaveStartTime >= ss && leaveEndTime <= ee)
            {
                return 0;
            }
            throw new Exception("程式設計錯誤");
        }
    }
 
}

