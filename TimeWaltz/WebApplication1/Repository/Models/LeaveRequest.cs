﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

[Table("LeaveRequest")]
public partial class LeaveRequest
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("EmployeesID")]
    public int EmployeesId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndTime { get; set; }

    [Column("VacationDetailsID")]
    public int VacationDetailsId { get; set; }

    [StringLength(50)]
    public string Reason { get; set; }

    [Unicode(false)]
    public string FileRoute { get; set; }

    [Column("AgentEmployeeID")]
    public int AgentEmployeeId { get; set; }

    [Column("ApprovalEmployeeID")]
    public int ApprovalEmployeeId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal LeaveMinutes { get; set; }

    [ForeignKey("AgentEmployeeId")]
    [InverseProperty("LeaveRequestAgentEmployees")]
    public virtual Employee AgentEmployee { get; set; }

    [ForeignKey("ApprovalEmployeeId")]
    [InverseProperty("LeaveRequestApprovalEmployees")]
    public virtual Employee ApprovalEmployee { get; set; }

    [ForeignKey("EmployeesId")]
    [InverseProperty("LeaveRequestEmployees")]
    public virtual Employee Employees { get; set; }

    [ForeignKey("VacationDetailsId")]
    [InverseProperty("LeaveRequests")]
    public virtual VacationDetail VacationDetails { get; set; }
}