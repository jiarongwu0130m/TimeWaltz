using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class UserOfDepartment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual User? User { get; set; }
}
