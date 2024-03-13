using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entity;

public partial class LeaveRequest
{
    public int Id { get; set; }

    public int EmployeesId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int VacationDetailsId { get; set; }
    [NotMapped]
    public string VacationType { get; set; }

    public string? Reason { get; set; }

    public string? FileRoute { get; set; }

    public int AgentEmployeeId { get; set; }
    [NotMapped]
    public string AgentEmployeeName { get; set; }

    public int ApprovalEmployeeId { get; set; }
    [NotMapped]
    public string ApporvalEmpName { get; set; }

    public int LeaveHours { get; set; }
    [NotMapped]
    public RequestStatusEnum ApprovalStatus { get; set; }

    public virtual Employee AgentEmployee { get; set; } = null!;

    public virtual Employee ApprovalEmployee { get; set; } = null!;

    public virtual ICollection<CompLeaveUseRecord> CompLeaveUseRecords { get; set; } = new List<CompLeaveUseRecord>();

    public virtual Employee Employees { get; set; } = null!;

    public virtual VacationDetail VacationDetails { get; set; } = null!;
}
