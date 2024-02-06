using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class CompLeaveUseRecord
{
    public int Id { get; set; }

    public int LeaveRequestId { get; set; }

    public int UsedTime { get; set; }

    public int CompLeaveId { get; set; }

    public virtual CompLeave CompLeave { get; set; } = null!;

    public virtual LeaveRequest LeaveRequest { get; set; } = null!;
}
