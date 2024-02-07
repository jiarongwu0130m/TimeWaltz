using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class ShiftSchedule
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ShiftsName { get; set; } = null!;

    public int BreakTime { get; set; }

    public int MaxAdditionalClockIn { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
