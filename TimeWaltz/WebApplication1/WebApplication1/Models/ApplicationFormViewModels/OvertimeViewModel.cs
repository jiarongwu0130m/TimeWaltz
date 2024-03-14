using WebApplication1.Models.Entity;

namespace WebApplication1.Models.ApplicationFormViewModels
{
    public class OvertimeViewModel
    {
        public int Id { get; set; }
        public DateTime OvertimeDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }

    public class OvertimeCreateViewModel
    {

        public int EmployeesId { get; set; } = 1;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Reason { get; set; } 

        public bool Status { get; set; }

        public int ApprovalEmployeeId { get; set; } = 2;
    }

    public class EmpIdNameGet
    {
        public int EmployeeId { get; set;}
        public string EmployeeName { get; set; }
    }
}
