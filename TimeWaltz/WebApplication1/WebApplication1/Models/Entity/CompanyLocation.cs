using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class CompanyLocation
{
    public int Id { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string Name { get; set; } = null!;
}
