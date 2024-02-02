using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class AdditionalClockIn
{
    public int Id { get; set; }

    public int EmployeesId { get; set; }

    public DateTime AdditionalTime { get; set; }

    public int Status { get; set; }

    public string? Reason { get; set; }

    public int ApprovalEmployeeId { get; set; }

    public virtual Employee ApprovalEmployee { get; set; } = null!;

    public virtual Employee Employees { get; set; } = null!;
}
