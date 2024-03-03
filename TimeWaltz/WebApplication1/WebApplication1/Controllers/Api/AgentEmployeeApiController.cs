using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AgentEmployeeApiController : ControllerBase
    {
        private readonly AgentEmployeeService _agentEmployeeService;

        public AgentEmployeeApiController(AgentEmployeeService agentEmployeeService)
        {
            _agentEmployeeService = agentEmployeeService;
        }
        public AgentEmploeeViewModel DropDownList()
        {
            var UserId = 1;
            var data = _agentEmployeeService.GetAgentDropDownData(UserId);
            var model = new AgentEmploeeViewModel
            {
                AgentEmployeeSelectItems = DropDownHelper.GetAgentDropDownList(data),
            };
            return model;
        }
    }
}
