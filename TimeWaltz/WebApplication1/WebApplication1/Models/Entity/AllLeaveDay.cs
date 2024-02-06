using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class AllLeaveDay
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int WeddingLeave { get; set; }

    public int BereavementLeave { get; set; }

    public int SickLeave { get; set; }

    public int PersonalLeave { get; set; }

    public int PeriodLeave { get; set; }

    public int MaternityLeave { get; set; }

    public int MaternityCheckUpLeave { get; set; }

    public int Special { get; set; }

    public int CompLeave { get; set; }
}
