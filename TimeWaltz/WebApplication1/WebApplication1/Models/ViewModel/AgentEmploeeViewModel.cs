using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entity;

namespace WebApplication1.Models.ViewModel
{
    public class AgentEmploeeViewModel
    {
        public int Id { get; set; }

        public int EmployeesId { get; set; }

        public int AgentEmployeesId { get; set; }

        public List<SelectListItem> AgentEmployeeSelectItems { get; set; }
        public string? AgentEmployeeName { get; set; }

        public List<SelectListItem>? AgentSelectItems { get; set; }
    }
}
