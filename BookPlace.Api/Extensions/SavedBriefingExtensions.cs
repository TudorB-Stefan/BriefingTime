using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;

namespace BookPlace.Api.Extensions;

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