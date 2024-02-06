using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class PublicHoliday
{
    public int Id { get; set; }

    public string HolidayName { get; set; } = null!;

    public DateTime Date { get; set; }
}
