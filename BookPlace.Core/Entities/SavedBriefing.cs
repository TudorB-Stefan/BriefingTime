namespace BookPlace.Core.Entities;

public class SavedBriefing
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string UserId { get; set; }
    public User User { get; set; }
    
    public string BriefingId { get; set; }
    public Briefing Briefing { get; set; }
}