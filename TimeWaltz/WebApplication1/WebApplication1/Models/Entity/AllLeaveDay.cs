using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class AllLeaveDay
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int VacationDetailsId { get; set; }

    public int? VacationHoursRemain { get; set; }
}
