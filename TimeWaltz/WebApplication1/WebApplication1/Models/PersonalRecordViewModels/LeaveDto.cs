using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models.PersonalRecordViewModels
{

    public class LeaveDto
    {
        public int EmployeesId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int VacationDetailsId { get; set; }

        public string? Reason { get; set; }
        public string? RelativeFileRoute { get; set; }

        public IFormFile? FileRoute { get; set; }

        public int AgentEmployeeId { get; set; }
        public int LeaveHours { get; set; }
    }

    public class LeaveCreateDto
    {
        public int EmployeesId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int VacationDetailsId { get; set; }

        public string? Reason { get; set; }

        public IFormFile? FileRoute { get; set; }
        public string? RelativeFileRoute { get; set; }

        public int AgentEmployeeId { get; set; }
        public int LeaveHours { get; set; }
    }

    public class LeaveDropDownDto
    {

        public List<SelectListItem> VacationDropDownList { get; set; }
        public List<SelectListItem> AgentDropDownList { get; set; }
    }

    public class EmployeeAndIdPareDto
    {
        public int EmployeeId { get; set;}
        public string EmployeesName { get; set; }
    }
}
