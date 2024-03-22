using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.PersonalRecordViewModels;

namespace WebApplication1.Services
{
    public class CompRequestService
    {
        private readonly TimeWaltzContext _db;

        public CompRequestService(TimeWaltzContext db)
        {
            _db = db;
        }
        
        public List<AdditionalClockIn> GetCompRequest()
        {
            return _db.AdditionalClockIns.ToList();
        }

        public List<AdditionalClockIn> GetSelectedCompRequest(CompRequestViewModel selectModel)
        {
            if (selectModel.QueryCompRequest != null)
            {
                return _db.AdditionalClockIns.Where(x => x.AdditionalTime >= selectModel.QueryCompRequest).ToList();
            }
            else
            {
                return _db.AdditionalClockIns.ToList();
            }
        }
        public AdditionalClockIn? GetCompRequestTypeOrNull(int id)
        {
            return _db.AdditionalClockIns.FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// 取得簽核人資料
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public int GetApprovalEmp(int empId)
        {
            return _db.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == empId).Department.EmployeeId;
        }
        /// <summary>
        /// 取得補打卡單List
        /// </summary>
        /// <param name="empId"></param>
        
    }
}
