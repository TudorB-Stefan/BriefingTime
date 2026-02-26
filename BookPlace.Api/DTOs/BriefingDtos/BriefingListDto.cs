using Microsoft.AspNetCore.Components.Web;

namespace BookPlace.Api.DTOs;

public class BriefingListDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}