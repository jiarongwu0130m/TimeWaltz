
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            return _timeWaltzContext.Employees.Find(userId);
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
                .Join(_timeWaltzContext.RequestStatuses, lveda => lveda.lved.lve.lv.l.Id, r => r.TableId, (lveda, r) => new { lveda, r })
                .Where(lvedar => (int)lvedar.r.TableType == 1).Select(lvedar => new LeaveRequest
                {
                    AgentEmployeeName = lvedar.lveda.a.Name,
                    VacationType = lvedar.lveda.lved.lve.lv.v.VacationType,
                    ApporvalEmpName = lvedar.lveda.lved.lve.e.Name,
                    StartTime = lvedar.lveda.lved.lve.lv.l.StartTime,
                    EmployeesId = lvedar.lveda.lved.lve.lv.l.EmployeesId,
                    EndTime = lvedar.lveda.lved.lve.lv.l.EndTime,
                    Id = lvedar.lveda.lved.lve.lv.l.Id,
                    ApprovalStatus = lvedar.r.Status,
                }).ToList();
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
        public LeaveRequest GetEditData(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

