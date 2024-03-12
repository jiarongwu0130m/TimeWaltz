using WebApplication1.Models.Entity;

namespace WebApplication1.Models.ApplicationFormViewModels
{
    public class OvertimeViewModel
    {
    }

    public class OvertimeCreateViewModel
    {
        public int Id { get; set; }

        public int EmployeesId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string? Reason { get; set; } 

        public bool Status { get; set; }

        public int ApprovalEmployeeId { get; set; }
    }
}
