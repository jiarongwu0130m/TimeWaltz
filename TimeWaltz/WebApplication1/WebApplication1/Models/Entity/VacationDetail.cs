using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class VacationDetail
{
    public int Id { get; set; }

    public string VacationType { get; set; } = null!;

    public bool Gender { get; set; }

    public int NumberOfDays { get; set; }

    public int Cycle { get; set; }

    public decimal MinVacationDays { get; set; }

    public virtual ICollection<CompRequest> CompRequests { get; set; } = new List<CompRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
