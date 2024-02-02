using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class SpacialVacation
{
    public int Id { get; set; }

    public string SpacialVacationName { get; set; } = null!;

    public DateTime Date { get; set; }
}
