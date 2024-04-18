using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CybageConnect.Models;

public partial class CybageConnectDbContext : DbContext
{
    public CybageConnectDbContext()
    {
    }

    public CybageConnectDbContext(DbContextOptions<CybageConnectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProject> UserProjects { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PRABHUDDHAB-GIF\\MSSQLSERVER2019;Database=CybageConnectDB;User Id=sa;Password=cybage@123456;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27CECD501C");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC27DDDA6D56");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("fk_comment_delete");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FriendId }).HasName("PK__Connecti__3DA43AFACEED4ED1");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.FriendId).HasColumnName("FriendID");
            entity.Property(e => e.ConnectionDate).HasColumnType("datetime");
            entity.Property(e => e.ConnectionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Friend).WithMany(p => p.ConnectionFriends)
                .HasForeignKey(d => d.FriendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Connections_Users2");

            entity.HasOne(d => d.User).WithMany(p => p.ConnectionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Connections_Users1");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Experien__3214EC27269D08ED");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Experiences_Users");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Likes__3214EC2781CFE37D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Post).WithMany(p => p.Likes)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("fk_like_delete");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_Users");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Posts__3214EC27CCE06B71");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Posts_Categories");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_Users");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC2744A6C5C9");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Skills__3214EC27D09C8EE7");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SkillName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27B0217642");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4AE82B7FA").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053408878086").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Departament)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserProj__3214EC271CA9DB79");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Project).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_UserProjects_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserProjects_Users");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSkil__3214EC27F22A7540");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SkillId).HasColumnName("SkillID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills)
                .HasForeignKey(d => d.SkillId)
                .HasConstraintName("FK_UserSkills_Skills");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkills)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserSkills_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
