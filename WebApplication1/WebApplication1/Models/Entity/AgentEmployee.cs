using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class AgentEmployee
{
    public int Id { get; set; }

    public int EmployeesId { get; set; }

    public int AgentEmployeesId { get; set; }

    public virtual Employee AgentEmployees { get; set; } = null!;

    public virtual Employee Employees { get; set; } = null!;
}
