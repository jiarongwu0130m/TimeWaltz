using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class LeaveRequest
{
    public int Id { get; set; }

    public int EmployeesId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int VacationDetailsId { get; set; }

    public string? Reason { get; set; }

    public string? FileRoute { get; set; }

    public int AgentEmployeeId { get; set; }

    public int ApprovalEmployeeId { get; set; }

    public virtual Employee AgentEmployee { get; set; } = null!;

    public virtual Employee ApprovalEmployee { get; set; } = null!;

    public virtual Employee Employees { get; set; } = null!;

    public virtual VacationDetail VacationDetails { get; set; } = null!;
}
