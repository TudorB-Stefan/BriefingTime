namespace BriefingTime.Api.DTOs;

public class SavedBriefingListDto
{
    public string BriefingId { get; set; }
    public string UserId { get; set; }
    public string BriefingTitle { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }
}