using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;

namespace BriefingTime.Api.Extensions;

public static class BriefingExtensions
{
    public static BriefingListDto ToListDto(this Briefing briefing)
    {
        return new BriefingListDto
        {
            Id = briefing.Id,
            Title = briefing.Title,
            Author = briefing.Author
        };
    }

    public static BriefingDetailDto ToDetailDto(this Briefing briefing)
    {
        return new BriefingDetailDto
        {
            Title = briefing.Title,
            Author = briefing.Author,
            Description = briefing.Description,
            FileSizeByte = briefing.FileSizeBytes,
            FileUrl = briefing.FileUrl,
            ContentType = briefing.ContentType,
            Comments = briefing.Comments.Select(c => c.ToDetailDto()).ToList()
        };
    }
}