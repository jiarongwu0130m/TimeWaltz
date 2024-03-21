﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

[Table("ShiftSchedule")]
public partial class ShiftSchedule
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndTime { get; set; }

    [Required]
    [StringLength(50)]
    public string ShiftsName { get; set; }

    public int BreakTime { get; set; }

    public int MaxAdditionalClockIn { get; set; }

    [InverseProperty("ShiftSchedule")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("ShiftSchedule")]
    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}