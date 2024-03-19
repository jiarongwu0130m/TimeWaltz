using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class SpecialVacation
{
    public int Id { get; set; }

    public string SpecialVacationName { get; set; } = null!;

    public DateTime Date { get; set; }
}
