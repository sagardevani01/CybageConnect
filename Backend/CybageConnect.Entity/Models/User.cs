using System;
using System.Collections.Generic;

namespace CybageConnect.Entity.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Designation { get; set; }

    public string? Departament { get; set; }

    public string? ProfilePicture { get; set; }

    public bool IsActive { get; set; }

    public string? Location { get; set; }

    public string? Phone { get; set; } 

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Connection> ConnectionFriends { get; set; } = new List<Connection>();

    public virtual ICollection<Connection> ConnectionUsers { get; set; } = new List<Connection>();

    public virtual ICollection<Experience> Experiences { get; set; } = new List<Experience>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();

    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}
