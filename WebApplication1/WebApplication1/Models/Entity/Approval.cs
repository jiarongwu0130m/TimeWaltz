using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Approval
{
    public int Id { get; set; }

    public int TableType { get; set; }

    public int TableId { get; set; }

    public bool? Status { get; set; }

    public string? Remark { get; set; }
}
