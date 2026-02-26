namespace BriefingTime.Core.Entities;

public class Briefing
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }

    public string FileUrl { get; set; }
    public string OriginalFileName { get; set; }
    public long FileSizeBytes { get; set; }
    public string ContentType { get; set; } = "application/pdf";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(24);
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    
    public string DepartmentId { get; set; }
    public Department Department { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }

    public ICollection<Comment> Comments { get; set; }
    public ICollection<SavedBriefing> SavedBriefings { get; set; }
}