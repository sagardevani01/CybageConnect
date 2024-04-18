using System;
using System.Collections.Generic;

namespace CybageConnect.Entity.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
