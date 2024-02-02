using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Billboard
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int EmployeesId { get; set; }

    public string? PathRoute { get; set; }

    public virtual Employee Employees { get; set; } = null!;
}
