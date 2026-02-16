using Microsoft.AspNetCore.Identity;

namespace BookPlace.Core.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Book> UploadedBooks { get; set; }
    public ICollection<DownloadLog> DownloadLogs { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<FavoriteBook> FavoriteBooks { get; set; }
}