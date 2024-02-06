using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; } = null!;

    public int EmployeesId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? DepartmentNavigation { get; set; }

    public virtual Employee Employees { get; set; } = null!;

    public virtual ICollection<Department> InverseDepartmentNavigation { get; set; } = new List<Department>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
