
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
        public LeaveDto GetRelativeFileRoute(LeaveDto model)
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
    }
}
