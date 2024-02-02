using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Clock
{
    public int Id { get; set; }

    public int EmployeesId { get; set; }

    public DateTime Date { get; set; }

    public int Status { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public virtual Employee Employees { get; set; } = null!;
}
