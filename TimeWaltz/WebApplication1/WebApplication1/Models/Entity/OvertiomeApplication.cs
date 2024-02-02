using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class OvertiomeApplication
{
    public int Id { get; set; }

    public int EmployeesId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Reason { get; set; } = null!;

    public bool Status { get; set; }

    public int ApprovalEmployee { get; set; }

    public virtual ICollection<CompRequest> CompRequests { get; set; } = new List<CompRequest>();

    public virtual Employee Employees { get; set; } = null!;
}
