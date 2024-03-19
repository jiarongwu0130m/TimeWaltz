using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models.Entity;

public partial class Shift
{
    public int Id { get; set; }

    public DateTime ShiftsDate { get; set; }
    [NotMapped]
    public string StartTime { get; set; }
    [NotMapped]
    public string EndTime { get; set; }
    [NotMapped]
    public string ShiftScheduleName { get; set; }
    [NotMapped]
    public string Title { get; set; }

    public int EmployeesId { get; set; }

    public int ShiftScheduleId { get; set; }

    public virtual Employee Employees { get; set; } = null!;

    public virtual ShiftSchedule ShiftSchedule { get; set; } = null!;

    [NotMapped]
    [JsonIgnore]
    public DateTime ShiftScheduleStartTime { get; set; }

    [NotMapped]
    [JsonIgnore]
    public DateTime ShiftScheduleEndTime { get; set; }
}
