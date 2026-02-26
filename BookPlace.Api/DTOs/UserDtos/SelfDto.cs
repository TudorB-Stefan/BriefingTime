namespace BookPlace.Api.DTOs;

public class SelfDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public ICollection<BriefingListDto> UploadedBriefings { get; set; }
    public ICollection<DownloadLogListDto> DownloadLogs { get; set; }
    public ICollection<SavedBriefingListDto> SavedBriefings { get; set; }
    public ICollection<CommentDetailDto> Comments { get; set; }
}