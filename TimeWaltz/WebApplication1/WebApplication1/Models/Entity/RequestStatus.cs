using System;
using System.Collections.Generic;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entity;

public partial class RequestStatus
{
    public int Id { get; set; }

    public TableTypeEnum TableType { get; set; }

    public int TableId { get; set; }

    public ApprovalStatusEnum Status { get; set; }
}
