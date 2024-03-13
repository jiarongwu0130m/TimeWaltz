using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.PersonalRecordViewModels
{

    public class LeaveDto
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int EmployeesId { get; set; }
        public string StartTime { get; set; }

        public string EndTime { get; set; }
        public int VacationDetailsId { get; set; }
        public string VacationType { get; set; }

        public int ApprovalEmployeeId { get; set; }
        public string ApprovalEmpName { get; set; }


        public int AgentEmployeeId { get; set; }
        public string AgentEmployeeName { get; set; }
        public int LeaveHours { get; set; }
        [NotMapped]
        public string ApprovalStatus { get; set; }
    }
    public class LeaveEditDto
    {
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string VacationType { get; set; }
        public string ApprovalEmpName { get; set; }
        public string AgentEmployeeName { get; set; }
        public int LeaveHours { get; set; }
        public string ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
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
        public int ApprovalEmployeeId { get; set; }
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
        public int EmployeesId { get; set;}
        public string EmployeesName { get; set; }
    }
}
