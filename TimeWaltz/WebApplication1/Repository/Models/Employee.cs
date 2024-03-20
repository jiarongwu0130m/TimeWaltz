﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Repository.Enum;

namespace Repository.Models;

public partial class Employee
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("ShiftScheduleID")]
    public int? ShiftScheduleId { get; set; }

    [Column("DepartmentID")]
    public int DepartmentId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime HireDate { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

    public int Gender { get; set; }

    [Required]
    [StringLength(50)]
    public string EmployeesNo { get; set; }

    [InverseProperty("Employees")]
    public virtual ICollection<AdditionalClockIn> AdditionalClockIns { get; set; } = new List<AdditionalClockIn>();

    [InverseProperty("Employees")]
    public virtual ICollection<Billboard> Billboards { get; set; } = new List<Billboard>();

    [InverseProperty("Employees")]
    public virtual ICollection<Clock> Clocks { get; set; } = new List<Clock>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Employees")]
    public virtual Department Department { get; set; }

    [InverseProperty("AgentEmployee")]
    public virtual ICollection<LeaveRequest> LeaveRequestAgentEmployees { get; set; } = new List<LeaveRequest>();

    [InverseProperty("ApprovalEmployee")]
    public virtual ICollection<LeaveRequest> LeaveRequestApprovalEmployees { get; set; } = new List<LeaveRequest>();

    [InverseProperty("Employees")]
    public virtual ICollection<LeaveRequest> LeaveRequestEmployees { get; set; } = new List<LeaveRequest>();

    [InverseProperty("Employees")]
    public virtual ICollection<OvertimeApplication> OvertimeApplications { get; set; } = new List<OvertimeApplication>();

    [ForeignKey("ShiftScheduleId")]
    [InverseProperty("Employees")]
    public virtual ShiftSchedule ShiftSchedule { get; set; }

    [InverseProperty("Employees")]
    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual User User { get; set; }

    [ForeignKey("EmployeesId")]
    [InverseProperty("Employees")]
    public virtual ICollection<Employee> AgentEmployees { get; set; } = new List<Employee>();

    [ForeignKey("AgentEmployeesId")]
    [InverseProperty("AgentEmployees")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("Employee")]
    public virtual ICollection<SpecialHolidayDays> SpecialHolidayDays { get; set; } = new List<SpecialHolidayDays>();
    
}