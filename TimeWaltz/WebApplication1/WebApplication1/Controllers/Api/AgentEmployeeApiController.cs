
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
            var AgentEmployeeSelectItems = DropDownHelper.GetAgentDropDownList(data);
            var agentEmployee = _agentEmployeeService.GetAgentemployee(UserId);
            foreach (var item in AgentEmployeeSelectItems)
            {
                if (agentEmployee.Contains(int.Parse(item.Value))){
                    item.Selected = true;
                }
            }
            var model = new AgentEmploeeViewModel
            {
            };
            return model;
        }
        [HttpPost]
        public bool EditAgentEmployee(AgentEmployeeDto dto)
        {
            try
            {
                _agentEmployeeService.EditAllAgentEmployee(dto);
                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
