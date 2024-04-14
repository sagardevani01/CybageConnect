using System;
using System.Collections.Generic;

namespace CybageConnect.Entity.Models;

public partial class Connection
{
    public int UserId { get; set; }

    public int FriendId { get; set; }

    public string? ConnectionStatus { get; set; }

    public DateTime? ConnectionDate { get; set; }

    public virtual User Friend { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
