using System;
using System.Collections.Generic;

namespace CybageConnect.Entity.Models;

public partial class Experience
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? CompanyName { get; set; }

    public string? Designation { get; set; }

    public int? ExperienceMonths { get; set; }

    public string? Description { get; set; }

    public virtual User User { get; set; } = null!;
}
