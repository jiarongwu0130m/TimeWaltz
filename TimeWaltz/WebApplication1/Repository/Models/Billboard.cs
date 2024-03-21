﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

[Table("Billboard")]
public partial class Billboard
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Title { get; set; }

    public string Content { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    [Column("EmployeesID")]
    public int EmployeesId { get; set; }

    public string PathRoute { get; set; }

    [ForeignKey("EmployeesId")]
    [InverseProperty("Billboards")]
    public virtual Employee Employees { get; set; }
}