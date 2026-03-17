using BriefingTime.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User> (options)
{
    public DbSet<Briefing> Briefings { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DownloadLog> DownloadLogs { get; set; }
    public DbSet<SavedBriefing> SavedBriefings { get; set; }
    public DbSet<UserDepartment> UserDepartments { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserDepartment>()
            .HasKey(ud => new { ud.UserId, ud.DepartmentId });
        builder.Entity<UserDepartment>()
            .HasOne(ud => ud.User)
            .WithMany(u => u.UserDepartments)
            .HasForeignKey(ud => ud.UserId);
        builder.Entity<UserDepartment>()
            .HasOne(ud => ud.Department)
            .WithMany(d => d.UserDepartments)
            .HasForeignKey(ud => ud.DepartmentId);
        builder.Entity<Briefing>()
            .HasOne(b => b.User)
            .WithMany(u => u.UploadedBriefing)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<SavedBriefing>()
            .HasKey(f => new { f.UserId, f.BriefingId });
        builder.Entity<SavedBriefing>()
            .HasOne(f => f.Briefing)
            .WithMany(b => b.SavedBriefings)
            .HasForeignKey(f => f.BriefingId);
        builder.Entity<DownloadLog>()
            .HasIndex(d => d.UserId);
        builder.Entity<Comment>()
            .HasOne(r => r.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}