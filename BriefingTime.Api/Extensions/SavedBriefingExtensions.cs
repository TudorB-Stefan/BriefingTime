using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;

namespace BriefingTime.Api.Extensions;

public static class SavedBriefingExtensions
{
    public static SavedBriefingListDto ToListDto(this SavedBriefing savedBriefingListDto)
    {
        return new SavedBriefingListDto
        {
            BriefingId = savedBriefingListDto.BriefingId,
            UserId = savedBriefingListDto.UserId,
            BriefingTitle = savedBriefingListDto.Briefing.Title,
            Author = savedBriefingListDto.Briefing.Author,
            CreatedAt = savedBriefingListDto.CreatedAt
        };
    }
}