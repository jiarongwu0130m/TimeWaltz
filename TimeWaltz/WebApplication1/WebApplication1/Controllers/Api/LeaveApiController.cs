using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Helpers;
using WebApplication1.Models.Dto;
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
    }
}
