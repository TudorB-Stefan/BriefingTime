using BookPlace.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User> (options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<DownloadLog> DownloadLogs { get; set; }
    public DbSet<FavoriteBook> FavoriteBooks { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Book>()
            .HasOne(b => b.User)
            .WithMany(u => u.UploadedBooks)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<FavoriteBook>()
            .HasKey(f => new { f.UserId, f.BookId });
        builder.Entity<FavoriteBook>()
            .HasOne(f => f.Book)
            .WithMany(b => b.FavoriteUsers)
            .HasForeignKey(f => f.BookId);
        builder.Entity<DownloadLog>()
            .HasIndex(d => d.UserId);
    }
}