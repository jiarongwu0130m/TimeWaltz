using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.ViewModel;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LeaveRequestApiController : ControllerBase
    {
        private readonly AgentEmployeeService _agentEmployeeService;
        private readonly LeaveService _leaveService;

        public LeaveRequestApiController(AgentEmployeeService agentEmployeeService, LeaveService leaveService)
        {
            _agentEmployeeService = agentEmployeeService;
            _leaveService = leaveService;
        }
        [HttpGet]
        public LeaveCreateViewModel DropDownList()
        {
            var UserId = 1;
            var agentData = _leaveService.GetAgentDropDownData(UserId);
            var AgentEmployeeSelectItems = DropDownHelper
                .GetAgentDropDownList(agentData);
            var VacationData = _leaveService.GetVacationDropDownData();
            var VacationTypeSelectItems = DropDownHelper
                .GetVacationTypeDropDownList(VacationData);
            var model = new LeaveCreateViewModel
            {
                AgentEmployeeSelectItems = AgentEmployeeSelectItems,
                VacationTypeSelectItems = VacationTypeSelectItems,                
            };
            return model;
        }
    }
}
