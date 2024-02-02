using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<AccessRoleBind> AccessRoleBinds { get; set; } = new List<AccessRoleBind>();

    public virtual ICollection<UserRoleBind> UserRoleBinds { get; set; } = new List<UserRoleBind>();
}
