using System;
using System.Collections.Generic;

namespace CybageConnect.Entity.Models;

public partial class Like
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
