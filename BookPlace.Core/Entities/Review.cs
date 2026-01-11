namespace BookPlace.Core.Entities;

public class Review
{
    public string Id { get; set; }
    public int Grade { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; }
    public User User { get; set; }

    public string BookId { get; set; }
    public Book Book { get; set; }
}