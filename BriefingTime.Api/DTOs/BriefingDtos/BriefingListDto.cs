using Microsoft.AspNetCore.Components.Web;

namespace BriefingTime.Api.DTOs;

public class BriefingListDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string DepartmentName { get; set; }
    public DateTime ExpiresAt { get; set; }
}