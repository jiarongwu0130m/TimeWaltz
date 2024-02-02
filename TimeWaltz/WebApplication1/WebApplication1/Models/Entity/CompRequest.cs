using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class CompRequest
{
    public int Id { get; set; }

    public int VacationDetailsId { get; set; }

    public int OvertimeId { get; set; }

    public int CompMin { get; set; }

    public virtual OvertiomeApplication Overtime { get; set; } = null!;

    public virtual VacationDetail VacationDetails { get; set; } = null!;
}
