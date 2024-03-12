using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.Json;
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

        public LeaveApiController(LeaveService leaveService, AgentEmployeeService agentEmployeeService)
        {
            _leaveService = leaveService;
            _agentEmployeeService = agentEmployeeService;
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
        public ActionResult Create(LeaveDto model)
        {
            try
            {
                var newmodel = _leaveService.GetRelativeFileRoute(model);
                var entity = ViewModelHelper.ToEntity(model);
                _leaveService.CreateLeaveRequest(entity);
                return Ok(new { status = true });

            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        
    }
}
