using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Repository.Enums;


namespace WebApplication1.Models.ApplicationFormViewModels
{

    public class CompRequestDto
    {
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public DateTime AdditionalTime { get; set; }

        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
    }

    public class CompRequestViewModel
    {
        public int Id { get; set; }

        public int EmployeesId { get; set; }

        [Display(Name = "補卡日期")]
        public DateTime AdditionalTime { get; set; }

        [Display(Name = "補卡時段")]
        public ClockStatusEnum Status { get; set; }

        public List<SelectListItem>? ClockStatusSelectItems { get; set; }

        public string? Reason { get; set; }

        public int ApprovalEmployeeId { get; set; }

        public DateTime? QueryCompRequest { get; set; }
    }

    public class CompRequestCreateViewModel
    {

        public int EmployeesId { get; set; }

        public DateTime AdditionalTime { get; set; }

        public ClockStatusEnum Status { get; set; }

        public List<SelectListItem>? ClockStatusSelectItems { get; set; }

        public string? Reason { get; set; }
        public int ApprovalEmployeeId { get; set; }

    }

    public class CompRequestEmpIdNameGet
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int approvalEmployeeid { get; set; }
        public string approvalEmployeeName { get; set; }

    }

    public class CompRequestApiCreateViewModel
    {

        public int EmployeesId { get; set; }

        public DateTime AdditionalTime { get; set; }

        public int Status { get; set; }


        public string? Reason { get; set; }

        public int ApprovalEmployeeId { get; set; }
    }
}
