using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class RequestStatus
{
    public int Id { get; set; }

    public int TableType { get; set; }

    public int TableId { get; set; }

    public int? Status { get; set; }
}
