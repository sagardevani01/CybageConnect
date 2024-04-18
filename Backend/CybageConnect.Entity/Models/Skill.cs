using System;
using System.Collections.Generic;

namespace CybageConnect.Entity.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string? SkillName { get; set; }

    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}
