using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entity;

namespace WebApplication1.Models.ApplicationFormViewModels
{
    public class OvertimeViewModel
    {
        public int Id { get; set; }        
        public DateTime OvertimeDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int EmployeesId { get; set; } = 1;
        public string Reason { get; set; }

        public bool Status { get; set; }

        public int ApprovalEmployeeId { get; set; } = 2;

    }

    public class OvertimeCreateViewModel
    {

        public int EmployeesId { get; set; } = 1;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public bool Status { get; set; }

        public int ApprovalEmployeeId { get; set; } = 2;
    }

    public class EmpIdNameGet
    {
        public int EmployeeId { get; set;}
        public string EmployeeName { get; set; }
    }
}
