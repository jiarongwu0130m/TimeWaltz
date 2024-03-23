using Microsoft.EntityFrameworkCore;
using Repository.Models;
namespace WebApplication1.Services
{
    public class OvertimeRequestService
    {
        private readonly TimeWaltzContext _db;
        private readonly ApprovalService _approvalService;

        public OvertimeRequestService(TimeWaltzContext db,ApprovalService approvalService)
        {
            _db = db;
            _approvalService = approvalService;
        }

        /// <summary>
        /// 取得簽核人資料
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        //public int GetApprovalEmp(int empId)//todo 部門主管
        //{
        //    return _db.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == empId).Department.EmployeeId;
        //}






        public OvertimeApplication CreateOvertimeRequest(OvertimeApplication entity)
        {
            _db.OvertimeApplications.Add(entity);
            _db.SaveChanges();

            _approvalService.NewApproval_加班單(entity.Id);
            return entity;
        }

        public List<OvertimeApplication> GetOvertimeList()
        {
            return _db.OvertimeApplications.ToList();
        }

        public OvertimeApplication? GetOvertimeRequestTypeOrNull(int id)
        {
            return _db.OvertimeApplications.FirstOrDefault(x => x.Id == id);

        }


        public Employee? GetNameOrNull(int id)
        {
            return _db.Employees.FirstOrDefault(x => x.Id == id);

        }
    }
}
