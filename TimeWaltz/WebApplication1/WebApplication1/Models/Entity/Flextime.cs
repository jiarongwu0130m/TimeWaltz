using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Flextime
{
    public int Id { get; set; }

    public int FlexibleTime { get; set; }

    public bool MoveUp { get; set; }
}
