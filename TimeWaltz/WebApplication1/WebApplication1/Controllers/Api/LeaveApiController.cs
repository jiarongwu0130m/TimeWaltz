using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using System.Drawing;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.PersonalRecordViewModels;
using WebApplication1.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "EmployeeAuthScheme")]

    public class LeaveApiController : ControllerBase
    {
        private readonly VacationTypeService _vacationTypeService;
        private readonly TimeWaltzContext _db;
        private readonly LeaveService _leaveService;
        private readonly RequestStatusService _requestStatusService;
        private readonly ApprovalService _approvalService;
        private readonly IWebHostEnvironment _env;


        public LeaveApiController(VacationTypeService vacationTypeService, TimeWaltzContext timeWaltzContext, LeaveService leaveService, RequestStatusService requestStatusService, ApprovalService approvalService, IWebHostEnvironment webHostEnvironment)
        {
            this._vacationTypeService = vacationTypeService;
            this._db = timeWaltzContext;
            _leaveService = leaveService;
            this._requestStatusService = requestStatusService;
            this._approvalService = approvalService;
            this._env = webHostEnvironment;
        }
        /// <summary>
        /// 請假申請單畫面，取下拉式選單資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public LeaveDropDownDto GetDropDownList()
        {
            var UserId = User.GetId();

            var vacation = _vacationTypeService.GetVacationDetailsList();
            var agent = _db.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == UserId);

            var dto = new LeaveDropDownDto
            {
                AgentDropDownList = agent.Department.Employees.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name,
                }).ToList(),
                VacationDropDownList = vacation.Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.VacationType
                }).ToList(),
            };
            return dto;
        }
        /// <summary>
        /// 請假申請單頁面，取得請假當事人的員工ID和姓名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public EmpIdNameGet GetEmployeeName()
        {
            var UserId = User.GetId();
            var userName = _db.Employees.FirstOrDefault(x => x.Id == UserId).Name;
            var UserAndName = new EmpIdNameGet
            {
                EmployeeId = UserId,
                EmployeeName = userName,
            };

            return UserAndName;
        }
        /// <summary>
        /// 新增一筆請假，修改資料庫
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create([FromForm] LeaveCreateDto dto)
        {
            try
            {
                //在處理(多塞)從使用者那裡接收不到的資料時要再建一個新的更大的更多欄位的model才比較安全(正確)
                var model = new LeaveCreateModel
                {
                    EmployeesId = dto.EmployeesId,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    VacationDetailsId = dto.VacationDetailsId,
                    Reason = dto.Reason,
                    AgentEmployeeId = dto.AgentEmployeeId
                };

                var relativePath = _leaveService.GetRelativeFileRoute(dto.FileRoute);

                model.RelativeFileRoute = relativePath;

                var approvalEmp = _leaveService.GetApprovalEmp(model.EmployeesId);

                model.ApprovalEmployeeId = approvalEmp;

                model.LeaveMinutes = _leaveService.AddLeaveTime2(model);

                var entity = new LeaveRequest
                {
                    EmployeesId = model.EmployeesId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    VacationDetailsId = model.VacationDetailsId,
                    Reason = model.Reason,
                    FileRoute = model.RelativeFileRoute,
                    AgentEmployeeId = model.AgentEmployeeId,
                    ApprovalEmployeeId = model.ApprovalEmployeeId,
                    LeaveMinutes = model.LeaveMinutes
                };

                _leaveService.CreateLeaveRequest(entity);


                _requestStatusService.NewRequestStatus(entity.Id);
                _approvalService.NewApproval(entity.Id); 

                return Ok(new { status = true });

            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        [HttpGet]
        public ActionResult<LeaveDto> List()
        {
            try
            {
                var empId = User.GetId();
                var models = _leaveService.GetLeaveListData(empId);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<LeaveEditDto> Edit(int Id)
        {
            try
            {
                var dto = _leaveService.GetEditDataOrNull(Id);
                return Ok(dto);
            }
            catch
            {
                return Ok(new { status = false });
            }
        }
    }
}
