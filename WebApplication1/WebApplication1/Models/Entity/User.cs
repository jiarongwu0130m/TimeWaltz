using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class User
{
    public int Id { get; set; }

    public int? EmployeesId { get; set; }

    public int DepartmentId { get; set; }

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? PasswordDate { get; set; }

    public bool Stop { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Employee? Employees { get; set; }

    public virtual ICollection<UserRoleBind> UserRoleBinds { get; set; } = new List<UserRoleBind>();
}
