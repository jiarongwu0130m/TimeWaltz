﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;

namespace Repository.Models;

[Table("SpecialHoliday")]
public partial class SpecialHoliday
{
    [Key]
    public int Id { get; set; }

    public HowToGiveEnum? HowToGive { get; set; }
}