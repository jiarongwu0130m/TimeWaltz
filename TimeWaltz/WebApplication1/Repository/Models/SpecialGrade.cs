﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

[Table("SpecialGrade")]
public partial class SpecialGrade
{
    [Key]
    public int Id { get; set; }

    public int ServiceLength { get; set; }

    public int Days { get; set; }
}