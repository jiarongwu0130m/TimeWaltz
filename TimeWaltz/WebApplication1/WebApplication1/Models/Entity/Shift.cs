﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Shift
{
    public int Id { get; set; }

    public DateTime ShiftsDate { get; set; }

    public int EmployeesId { get; set; }

    public int ShiftScheduleId { get; set; }

    public virtual Employee Employees { get; set; } = null!;

    public virtual ShiftSchedule ShiftSchedule { get; set; } = null!;
}