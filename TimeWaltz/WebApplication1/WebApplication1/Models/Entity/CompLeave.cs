using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class CompLeave
{
    public int Id { get; set; }

    public int VacationDetailsId { get; set; }

    public int OvertimeId { get; set; }

    public int CompMin { get; set; }

    public virtual ICollection<CompLeaveUseRecord> CompLeaveUseRecords { get; set; } = new List<CompLeaveUseRecord>();

    public virtual OvertimeApplication IdNavigation { get; set; } = null!;
}
