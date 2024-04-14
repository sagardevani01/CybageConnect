using System;
using System.Collections.Generic;

namespace CybageConnect.Entity.Models;

public partial class Project
{
    public int Id { get; set; }

    public string? ProjectName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
