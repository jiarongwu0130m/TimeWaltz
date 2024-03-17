using Microsoft.Build.Framework;

namespace WebApplication1.Models.ApplicationFormViewModels
{
    public class OvertimeRequestDto
    {
        //public int EmployeesId { get; set; } = 1;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public bool Status { get; set; }

        //public int ApprovalEmployeeId { get; set; } = 2;

    }

}
