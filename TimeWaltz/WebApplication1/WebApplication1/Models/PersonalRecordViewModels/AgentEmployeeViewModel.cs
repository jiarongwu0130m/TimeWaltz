using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication1.Models.PersonalRecordViewModels
{
    public class AgentEmployeeViewModel
    {
        public int Id { get; set; }

        public int EmployeesId { get; set; }

        public int AgentEmployeesId { get; set; }

        public List<SelectListItem> AgentEmployeeSelectItems { get; set; }
        public string? AgentEmployeeName { get; set; }

        public List<SelectListItem>? AgentSelectItems { get; set; }
    }
}
