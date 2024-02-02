using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Access
{
    public int Id { get; set; }

    public string ManuName { get; set; } = null!;

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public virtual ICollection<AccessRoleBind> AccessRoleBinds { get; set; } = new List<AccessRoleBind>();
}
