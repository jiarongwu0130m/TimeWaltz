using System;
using System.Collections.Generic;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entity;

public partial class VacationDetail
{
    public int Id { get; set; }

    public VacationTypeEnum VacationType { get; set; }

    public bool? Gender { get; set; }

    public int NumberOfDays { get; set; }

    public CycleEnum Cycle { get; set; }

    public int MinVacationHours { get; set; }

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
