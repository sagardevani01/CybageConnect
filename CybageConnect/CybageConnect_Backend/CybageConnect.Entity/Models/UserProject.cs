using System;
using System.Collections.Generic;

namespace CybageConnect.Entity.Models;

public partial class UserProject
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; }
}
