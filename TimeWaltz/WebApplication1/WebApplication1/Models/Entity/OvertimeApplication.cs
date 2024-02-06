using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class OvertimeApplication
{
    public int Id { get; set; }

    public int EmployeesId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Reason { get; set; } = null!;

    public bool Status { get; set; }

    public int ApprovalEmployeeId { get; set; }

    public virtual Employee ApprovalEmployee { get; set; } = null!;

    public virtual CompLeave? CompLeave { get; set; }

    public virtual Employee Employees { get; set; } = null!;
}
