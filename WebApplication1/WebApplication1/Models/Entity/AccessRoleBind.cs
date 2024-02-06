using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class AccessRoleBind
{
    public int Id { get; set; }

    public int AccessId { get; set; }

    public int RoleId { get; set; }

    public virtual Access Access { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
