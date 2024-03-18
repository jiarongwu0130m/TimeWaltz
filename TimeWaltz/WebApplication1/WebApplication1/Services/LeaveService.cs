
using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using WebApplication1.Models.Entity;
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
            return _timeWaltzContext.LeaveRequests.Where(x => x.Id == Id)
                 .Join(_timeWaltzContext.VacationDetails, l => l.VacationDetailsId, v => v.Id, (l, v) => new { l, v })
                 .Join(_timeWaltzContext.Employees, lv => lv.l.EmployeesId, e => e.Id, (lv, e) => new { lv, e })
                 .Join(_timeWaltzContext.Departments, lve => lve.e.Id, d => d.EmployeesId, (lve, d) => new { lve, d })
                 .Join(_timeWaltzContext.Employees, lved => lved.lve.lv.l.AgentEmployeeId, a => a.Id, (lved, a) => new { lved, a })
                 .Join(_timeWaltzContext.Employees, lveda => lveda.lved.lve.lv.l.ApprovalEmployeeId, ap => ap.Id, (lveda, ap) => new { lveda, ap })
                 .Join(_timeWaltzContext.RequestStatuses, lvedaap => lvedaap.lveda.lved.lve.lv.l.Id, r => r.TableId, (lvedaap, r) => new { lvedaap, r })
                 .Join(_timeWaltzContext.Approvals, lvedaapr => lvedaapr.lvedaap.lveda.lved.lve.lv.l.Id, app => app.TableId, (lvedaapr, app) => new { lvedaapr, app })
                 .Where(lvedaaprapp => lvedaaprapp.app.TableType == (Models.Enums.TableTypeEnum)1)
                .Where(lvedaaprapp => (int)lvedaaprapp.lvedaapr.r.TableType == 1).Select(lvedaaprapp => new LeaveRequest
                {
                    VacationType = lvedaaprapp.lvedaapr.lvedaap.lveda.lved.lve.lv.v.VacationType,
                    AgentEmployeeName = lvedaaprapp.lvedaapr.lvedaap.lveda.a.Name,
                    ApprovalStatus = lvedaaprapp.lvedaapr.r.Status,
                    ApporvalEmpName = lvedaaprapp.lvedaapr.lvedaap.ap.Name,
                    Id = lvedaaprapp.lvedaapr.lvedaap.lveda.lved.lve.lv.l.Id,
                    StartTime = lvedaaprapp.lvedaapr.lvedaap.lveda.lved.lve.lv.l.StartTime,
                    EndTime = lvedaaprapp.lvedaapr.lvedaap.lveda.lved.lve.lv.l.EndTime,
                    EmployeeName = lvedaaprapp.lvedaapr.lvedaap.lveda.lved.lve.e.Name,
                    ApprovalRemark = lvedaaprapp.app.Remark,
                    Reason = lvedaaprapp.lvedaapr.lvedaap.lveda.lved.lve.lv.l.Reason,
                }).FirstOrDefault();
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
                .Join(_timeWaltzContext.ShiftSchedules, e => e.ShiftScheduleId, shiftSchedule => shiftSchedule.Id, (e, shiftSchedule) => new { e, shiftSchedule })
                .Join(_timeWaltzContext.Shifts, eshiftSchedule => eshiftSchedule.e.Id, shift=>shift.EmployeesId, (eshiftSchedule, shift)=> new { eshiftSchedule, shift})
                .FirstOrDefault(eshiftScheduleshift => eshiftScheduleshift.eshiftSchedule.e.Id == model.EmployeesId);
            if (emp == null) throw new Exception("程式錯誤");

            var Shifts = emp.eshiftSchedule.shiftSchedule.Shifts;

            //請假時間是否多於一天的判斷
            if (!IsSameDay(leaveStart, leaveEnd))
            {
                //請假開始時間及結束時間在不同天
                TimeSpan startTimeOfDay, endTimeOfDay;
                var fullDays = CalculateFullDays(leaveStart, leaveEnd, out startTimeOfDay, out endTimeOfDay);

                for(var i = 0; i < 5; i++) 
                {
                    leaveStart.AddDays(i);
                    var Have = Shifts.FirstOrDefault(x => x.ShiftsDate == leaveStart);
                    if(Have != null)
                    {
                        model.LeaveHours = model.LeaveHours + 8;
                    }
                }
                var ShiftB = Shifts.FirstOrDefault(x => x.ShiftsDate.Date == model.StartTime.Date);
                if (ShiftB != null)
                {
                    var shiftStart = emp.eshiftSchedule.shiftSchedule.StartTime;
                    var shiftEnd = emp.eshiftSchedule.shiftSchedule.EndTime;
                    //確認請假開始時間即結束時間在同一天
                    //判斷請假時間與班別時間的關係取得符合班別上下班時間的請假時間
                    var leaveStartTime = GetCorrectStartTime(leaveStart.TimeOfDay, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                    var leaveEndTime = GetCorrectEndTime(leaveStartTime, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                    //算出扣除掉午休時間的請假時間
                    var leaveTime = GetLeaveTimeSkipBreakTime(leaveStartTime, leaveEndTime);

                    model.LeaveHours = leaveTime / 60;
                    return model;
                }                
                var ShiftC = Shifts.FirstOrDefault(x => x.ShiftsDate.Date == model.EndTime.Date);
                if(ShiftC != null)
                {
                    var shiftStart = emp.eshiftSchedule.shiftSchedule.StartTime;
                    var shiftEnd = emp.eshiftSchedule.shiftSchedule.EndTime;
                    //確認請假開始時間即結束時間在同一天
                    //判斷請假時間與班別時間的關係取得符合班別上下班時間的請假時間
                    var leaveStartTime = GetCorrectStartTime(leaveStart.TimeOfDay, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                    var leaveEndTime = GetCorrectEndTime(leaveStartTime, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                    //算出扣除掉午休時間的請假時間
                    var leaveTime = GetLeaveTimeSkipBreakTime(leaveStartTime, leaveEndTime);

                    model.LeaveHours = leaveTime / 60;
                    return model;
                }
            }
            else
            {
                //判斷請假單日期在班表上是否有班
                var ShiftA = Shifts.FirstOrDefault(x => x.ShiftsDate == model.StartTime.Date);
                if(ShiftA == null)
                {
                    model.LeaveHours = 0;
                    return model;
                }
                var shiftStart = emp.eshiftSchedule.shiftSchedule.StartTime;
                var shiftEnd = emp.eshiftSchedule.shiftSchedule.EndTime;
                //確認請假開始時間即結束時間在同一天
                //判斷請假時間與班別時間的關係取得符合班別上下班時間的請假時間
                var leaveStartTime = GetCorrectStartTime(leaveStart.TimeOfDay, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                var leaveEndTime = GetCorrectEndTime(leaveStartTime, leaveEnd.TimeOfDay, shiftStart.TimeOfDay, shiftEnd.TimeOfDay);
                //算出扣除掉午休時間的請假時間
                var leaveTime = GetLeaveTimeSkipBreakTime(leaveStartTime, leaveEndTime);

                model.LeaveHours = leaveTime / 60;
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
                leaveStart = leaveEnd;
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
                return (ss - leaveStartTime).Minutes;
            }
            else if(leaveStartTime < ss && leaveEndTime <= ss || leaveEndTime > ee && leaveStartTime >= ee)
            {
                return (leaveEndTime - leaveStartTime).Minutes;
            }
            else if(leaveStartTime < ss && leaveEndTime > ee)
            {
                return (ss - leaveStartTime).Minutes + (leaveEndTime - ee).Minutes;
            }
            else if(leaveEndTime > ee && leaveStartTime < ee && leaveStartTime >= ss)
            {
                return (leaveEndTime - ee).Minutes;
            }
            else if(leaveStartTime >= ss && leaveEndTime <= ee)
            {
                return 0;
            }
            throw new Exception("程式設計錯誤");
        }
    }
 
}

