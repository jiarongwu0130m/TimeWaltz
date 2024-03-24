using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using WebApplication1.Helpers;
using WebApplication1.Models.ApplicationFormViewModels;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Areas.Employee.Controllers.Api
{
    [Route("mobile/api/Complement/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]
    public class ComplementApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;
        private readonly CompRequestService _compRequestService;
        private readonly ApprovalService _approvalService;

        public ComplementApiController(TimeWaltzContext db, CompRequestService compRequestService, ApprovalService approvalService)
        {
            _db = db;
            _compRequestService = compRequestService;
            _approvalService = approvalService;
        }
        /// <summary>
        /// 補打卡單申請
        /// </summary>
        /// <param name="model">CompRequestCreateDto</param>
        /// <returns></returns>
        [HttpPost]
        public bool CompRequestCreate(CompRequestCreateDto model)
        {
            try
            {                
                var entity = new AdditionalClockIn
                {
                    EmployeesId = model.EmployeesId,
                    ApprovalEmployeeId = model.ApprovalEmployeeId,                    
                    AdditionalTime = model.AdditionalTime,
                    Status = (int)model.Status,
                    Reason = model.Reason,
                };

                _db.AdditionalClockIns.Add(entity);
                _db.SaveChanges();
                //新增補打卡單簽核
                _approvalService.NewApproval_補打卡(entity.Id);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       

        /// <summary>
        /// 取得補打卡單所有單據
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<CompRequestListDto> List()
        {
            try
            {
                var empId = User.GetId();

                //取得補打卡單List
                var listModels = _db.AdditionalClockIns
                .Where(addClock => addClock.EmployeesId == empId)
                .Join(_db.Approvals
                      .Where(
                             appr => appr.TableType == (int)TableTypeEnum.補卡單),
                             addClock => addClock.Id,
                             appr => appr.TableId,
                             (addClock, appr) => new { addClock, appr }
                             )
                .Select(compList => new CompRequestListDto
                {
                    Id = compList.addClock.Id,
                    EmployeesId = compList.addClock.EmployeesId,
                    AdditionalTime = compList.addClock.AdditionalTime,
                    Status = compList.addClock.Status.ToString(),
                    ApprovalStatus = compList.appr.Status.ToString(),
                }).ToList();

                return Ok(listModels);
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }

        /// <summary>
        /// 單據詳細資料，頁面僅供檢視
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<CompRequestDetailDto> GetCompRequestDetail(int Id)
        {
            try
            {
                var compRequest = _db.AdditionalClockIns
                .Include(x => x.Employees)
                .Join(_db.Employees,
                x => x.ApprovalEmployeeId,
                y => y.Id,
                (x, y) => new { x, y })
                .Join(_db.Approvals.Where(
                    xy => xy.TableType == (int)TableTypeEnum.補卡單),
                    xy => xy.x.Id,
                    y => y.TableId,
                    (xy, z) => new { xy, z }
                    )
                .FirstOrDefault(xyz => xyz.xy.x.Id == Id);
                if (compRequest == null) throw new NullReferenceException("Not find this user");
                var result = new CompRequestDetailDto
                {
                    Id = compRequest.xy.x.Id,
                    EmployeeName = compRequest.xy.x.Employees.Name,
                    AdditionalTime = compRequest.xy.x.AdditionalTime,
                    Status = compRequest.xy.x.Status.ToString(),
                    Reason = compRequest.xy.x.Reason,
                    ApprovalEmpName = compRequest.xy.y.Name,
                    ApprovalStatus = compRequest.z.Status == null ? "" : compRequest.z.Status.ToString(),
                    ApprovalRemark = compRequest.z.Remark == null ? "" : compRequest.z.Remark,
                };
                return result;
            }
            catch
            {
                return Ok(new { status = false });
            }
        }
        /// <summary>
        /// 補打卡單新增資料_取得補卡狀態選單
        /// </summary>
        /// <returns></returns>
        public CompRequestCreateDto DropDownList()
        {
            var clockEnum = Enum.GetValues(typeof(ClockStatusEnum)).Cast<ClockStatusEnum>().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToList();

            var model = new CompRequestCreateDto
            {
                ClockStatusSelectItems = clockEnum,
            };


            return model;
        }
        /// <summary>
        /// 補打卡單新增資料_取得請假當事人的員工ID和姓名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CompRequestGetEmpIdName GetEmployeeName()
        {
            var UserId = User.GetId();
            var userName = _db.Employees.FirstOrDefault(x => x.Id == UserId).Name;
            var UserAndName = new CompRequestGetEmpIdName
            {
                EmployeeId = UserId,
                EmployeeName = userName,
            };

            return UserAndName;
        }

        /// <summary>
        ///取得員工打卡紀錄
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetEmpClocks()
        {
            var clocks = _db.Clocks.AsNoTracking()
                      .Where(x => x.EmployeesId == User.GetId())
                      .ToList()
                      .GroupBy(x => x.Date.Date)
                      .Select(g =>
                      {
                          var date = g.Key.ToString("yyyy-MM-dd");
                          var on = g.FirstOrDefault(x => x.Status == ClockStatusEnum.上班打卡)?.Date.ToString("HH:mm:ss");
                          var off = g.Where(x => x.Status == ClockStatusEnum.下班打卡)
                                     .OrderByDescending(x => x.Date)
                                     .FirstOrDefault()?.Date.ToString("HH:mm:ss");
                          return new { date, on, off };
                      })
                      .ToList();

            if (clocks == null)
            {
                return NotFound();
            }

            return Ok(clocks);
        }

    }
}
