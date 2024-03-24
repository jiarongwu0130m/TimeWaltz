using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.PersonalRecordViewModels
{

    public class LeaveDto
    {
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public string Date { get; set; }
        public string VacationType { get; set; }
        public string ApprovalStatus { get; set; }
    }
    public class LeaveEditDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string TimeRange { get; set; }
        public string VacationType { get; set; }
        public string ApprovalEmpName { get; set; }
        public string AgentEmployeeName { get; set; }
        public decimal LeaveMinutes { get; set; }
        public string ApprovalStatus { get; set; }
        public string? Reason { get; set; }
        public string? ApprovalRemark { get; set; }
        public int LeaveHours { get; internal set; }
    }

    public class LeaveCreateDto
    {
        public int ApprovalEmployeeId { get; set; }
        public int EmployeesId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int VacationDetailsId { get; set; }

        public string? Reason { get; set; }

        public IFormFile? FileRoute { get; set; }
        public int AgentEmployeeId { get; set; }
    }
    public class LeaveCreateModel
    {
        public int EmployeesId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int VacationDetailsId { get; set; }

        public string? Reason { get; set; }

        public string? RelativeFileRoute { get; set; }
        public int ApprovalEmployeeId { get; set; }

        public int AgentEmployeeId { get; set; }
        public decimal LeaveMinutes { get; set; }
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
