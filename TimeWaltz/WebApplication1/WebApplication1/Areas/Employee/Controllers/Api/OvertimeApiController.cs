using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Services;

namespace WebApplication1.Areas.Employee.Controllers.Api
{
    [Route("mobile/api/Overtime/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    public class OvertimeApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;
        private readonly OvertimeRequestService _overtimeRequestService;
        private readonly ApprovalService _approvalService;

        public OvertimeApiController(TimeWaltzContext db,OvertimeRequestService overtimeRequestService,ApprovalService approvalService)
        {
            _db = db;
            _overtimeRequestService = overtimeRequestService;
            _approvalService = approvalService;
        }

        /// <summary>
        /// 加班單新增資料_取得加班當事人的員工ID和姓名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public OvertimeRequestGetEmpIdName GetEmployeeName()
        {
            var UserId = User.GetId();
            var userName = _db.Employees.FirstOrDefault(x => x.Id == UserId).Name;
            var UserAndName = new OvertimeRequestGetEmpIdName
            {
                EmployeeId = UserId,
                EmployeeName = userName,
            };

            return UserAndName;
        }

        /// <summary>
        /// 加班單申請
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public bool RequestCreate(OvertimeRequestCreateDto model)
        {
            try
            {
                var approvalEmp = _overtimeRequestService.GetApprovalEmp(model.EmployeeId);
                model.ApprovalEmployeeId = approvalEmp;

                var entity = new OvertimeApplication
                {
                    EmployeesId = model.EmployeeId,
                    ApprovalEmployeeId = model.ApprovalEmployeeId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Status = model.Status,
                    Reason = model.Reason                    
                };
                _db.OvertimeApplications.Add(entity);
                _db.SaveChanges();

                //新增補打卡單簽核
                _approvalService.NewApproval_加班單(entity.Id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 取得加班單所有單據
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<OvertimeListDto> List()
        {
            try
            {
                var empId = User.GetId();

                //取得補打卡單List
                var listModels = _db.OvertimeApplications
                .Where(overtime => overtime.EmployeesId == empId)
                .Join(_db.Approvals
                      .Where(
                             appr => appr.TableType == (int)TableTypeEnum.加班單),
                             overtime => overtime.Id,
                             appr => appr.TableId,
                             (overtime, appr) => new { overtime, appr }
                             )
                .Select(overtimeList => new OvertimeListDto
                {
                    Id = overtimeList.overtime.Id,
                    EmployeesId = overtimeList.overtime.EmployeesId,
                    StartTime = overtimeList.overtime.StartTime,
                    EndTime = overtimeList.overtime.EndTime,                    
                    ApprovalStatus = overtimeList.appr.Status.ToString(),
                }).ToList();

                return Ok(listModels);
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }



    }
}
