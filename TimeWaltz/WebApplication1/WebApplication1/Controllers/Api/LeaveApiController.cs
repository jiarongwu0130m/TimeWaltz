using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication1.Helpers;
using WebApplication1.Models.Entity;
using WebApplication1.Models.PersonalRecordViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LeaveApiController : ControllerBase
    {
        private readonly LeaveService _leaveService;
        private readonly AgentEmployeeService _agentEmployeeService;
        private readonly RequestStatusService _requestStatusService;
        private readonly ApprovalService _approvalService;

        public LeaveApiController(LeaveService leaveService, AgentEmployeeService agentEmployeeService, RequestStatusService requestStatusService, ApprovalService approvalService)
        {
            _leaveService = leaveService;
            _agentEmployeeService = agentEmployeeService;
            this._requestStatusService = requestStatusService;
            this._approvalService = approvalService;
        }
        /// <summary>
        /// 請假申請單畫面，取下拉式選單資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public LeaveDropDownDto GetDropDownList()
        {
            var UserId = 1;
            var vacation = _leaveService.GetVacationDropDownData();
            var agent = _agentEmployeeService.GetAgentDropDownData(UserId);
            
            var model = new LeaveDropDownDto 
            {
                AgentDropDownList = DropDownHelper.GetAgentDropDownList(agent),
                VacationDropDownList = DropDownHelper.GetVacationTypeDropDownList(vacation),
            };
            return model;
        }
        /// <summary>
        /// 請假申請單頁面，取得請假當事人的員工ID和姓名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public EmployeeAndIdPareDto GetEmoloyeeName()
        {
            var UserId = 1;
            var userNameAndIdPare = _leaveService.GetNameOrNull(UserId);
            var employee = EntityHelper.GetNameAndIdPare(userNameAndIdPare);
            

            return employee;
        }
        /// <summary>
        /// 新增一筆請假，修改資料庫
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create([FromForm]LeaveCreateDto model)
        {
            try
            {
                var fileModel = _leaveService.GetRelativeFileRoute(model);
                var approvalModel = _leaveService.GetApprovalEmp(fileModel);
                var entity = ViewModelHelper.ToEntity(approvalModel);
                _leaveService.CreateLeaveRequest(entity);
                //TODO: 新增狀態表
                _requestStatusService.NewRequestStatus(entity);
                //TODO: 新增簽核表
                _approvalService.NewApproval(entity);
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
                var entities = _leaveService.GetLeaveListData();
                var models = EntityHelper.ToDto(entities);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
            
        }
        [HttpGet]
        public ActionResult<LeaveEditDto> Edit(int Id)
        {
            var entity = _leaveService.GetEditData(Id);
            var model = EntityHelper.ToDto(entity);
            return Ok();
        }
    }
}
