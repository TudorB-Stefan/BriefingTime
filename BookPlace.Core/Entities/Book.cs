namespace BookPlace.Core.Entities;

public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Publisher { get; set; }

    public string FileUrl { get; set; }
    public string OriginalFileName { get; set; }
    public long FileSizeBytes { get; set; }
    public string ContentType { get; set; } = "application/pdf";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    
    public string CategoryId { get; set; }
    public string Category { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }

    public double AvgGrade { get; set; }
    public ICollection<Review> Reviews { get; set; }
}