using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ApplicationFormViewModels
{
    /// <summary>
    /// 原始entity資料型別
    /// 資料表：OvertimeApplication
    /// </summary>
    public class OvertimeRequestDto
    {
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Reason { get; set; }
        public bool Status { get; set; }
        public int ApprovalEmployeeId { get; set; }

    }

    public class OvertimeRequestCreateDto
    {
        public int EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Reason { get; set; }
        public bool Status { get; set; }
        public int ApprovalEmployeeId { get; set; }

    }

    public class OvertimeRequestGetEmpIdName
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }

}
