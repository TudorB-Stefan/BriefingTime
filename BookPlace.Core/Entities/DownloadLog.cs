namespace BookPlace.Core.Entities;

public class DownloadLog
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string UserId { get; set; }
    public User User { get; set; }

    public string BookId { get; set; }
    public Book Book { get; set; }
}