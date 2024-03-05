using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entity;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int? EmployeesId { get; set; }
    [NotMapped]
    public string? EmployeeName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
