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

    public string Salt { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual UserOfDepartment Id1 { get; set; } = null!;

    public virtual Employee IdNavigation { get; set; } = null!;

    public virtual ICollection<UserRoleBind> UserRoleBinds { get; set; } = new List<UserRoleBind>();
}
