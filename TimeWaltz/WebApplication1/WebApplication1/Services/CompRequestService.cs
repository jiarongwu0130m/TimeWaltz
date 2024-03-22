using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.PersonalRecordViewModels;

namespace WebApplication1.Services
{
    public class CompRequestService
    {
        private readonly TimeWaltzContext _timeWaltzDb;

        public CompRequestService(TimeWaltzContext timeWaltzDb)
        {
            _timeWaltzDb = timeWaltzDb;
        }        

        public AdditionalClockIn CreateCompRequest(AdditionalClockIn entity)
        {
            _timeWaltzDb.AdditionalClockIns.Add(entity);
            _timeWaltzDb.SaveChanges();
            return entity;
        }

        public List<AdditionalClockIn> GetCompRequest()
        {
            return _timeWaltzDb.AdditionalClockIns.ToList();
        }

        public List<AdditionalClockIn> GetSelectedCompRequest(CompRequestViewModel selectModel)
        {
            if (selectModel.QueryCompRequest != null)
            {
                return _timeWaltzDb.AdditionalClockIns.Where(x => x.AdditionalTime >= selectModel.QueryCompRequest).ToList();
            }
            else
            {
                return _timeWaltzDb.AdditionalClockIns.ToList();
            }
        }
        public AdditionalClockIn? GetCompRequestTypeOrNull(int id)
        {
            return _timeWaltzDb.AdditionalClockIns.FirstOrDefault(x => x.Id == id);
        }
        
        /// <summary>
        /// 取得簽核人資料
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public int GetApprovalEmp(int empId)
        {
            return _timeWaltzDb.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == empId).Department.EmployeeId;
        }

        public List<CompRequestDto> GetCompRequesListData(int empId)
        {
            return _timeWaltzDb.AdditionalClockIns
                .Where(x => x.EmployeesId == empId)
                .Join(_timeWaltzDb.Approvals.Where(y => y.TableType == (int)TableTypeEnum.補卡單), x => x.Id, y => y.TableId, (x, y) => new { x, y })
            .Select(xy => new CompRequestDto
            {
                Id = xy.x.Id,
                EmployeesId = xy.x.EmployeesId,
                AdditionalTime = xy.x.AdditionalTime,
                Status = xy.x.Status.ToString(),                
                ApprovalStatus = xy.y.Status.ToString(),
            }).ToList();
        }

    }
}
