namespace BriefingTime.Api.DTOs;

public class DownloadLogListDto
{
    public string BriefingId { get; set; }
    public string BriefingTitle { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public DateTime DownloadedAt { get; set; }
}