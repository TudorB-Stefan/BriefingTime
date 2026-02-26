using BriefingTime.Api.DTOs;
using BriefingTime.Api.DTOs.AuthDtos;
using BriefingTime.Api.DTOs.MemberDtos;
using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces;

namespace BriefingTime.Api.Extensions;

public static class UserExtensions
{
    public static SelfDto ToSelfDto(this User user)
    {
        return new SelfDto
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt,
            ModifiedAt = user.ModifiedAt,
            UploadedBriefings = user.UploadedBriefing?.Select(b => new BriefingListDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
            }).ToList() ?? new List<BriefingListDto>(),
            DownloadLogs = user.DownloadLogs?.Select(dlog => new DownloadLogListDto
            {
                BriefingId = dlog.BriefingId,
                BriefingTitle = dlog.Briefing?.Title ?? "Unknown title",
                UserId = dlog.UserId,
                UserName = dlog.User?.UserName ?? "Unknown username",
                DownloadedAt = dlog.CreatedAt
            }).ToList() ?? new List<DownloadLogListDto>(),
            SavedBriefings = user.SavedBriefings?.Select(fbook => new SavedBriefingListDto
            {
                BriefingId = fbook.BriefingId,
                BriefingTitle = fbook.Briefing.Title ?? "Unknown title",
                Author = fbook.Briefing.Author ?? "Unknown author",
                CreatedAt = fbook.CreatedAt
            }).ToList() ?? new List<SavedBriefingListDto>(),
            Comments = user.Comments?.Select(r => new CommentDetailDto
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                CreatedAt = r.CreatedAt,
                UserName = r.User?.UserName ?? "Unknown username",
                UserFirstName = r.User?.FirstName ?? "Unknown firstname",
                UserLastName = r.User?.LastName ?? "Unknown lastname"
            }).ToList() ?? new List<CommentDetailDto>(),
        };
    }
    public static async Task<AuthResponseDto> ToDto(this User user, ITokenService tokenService, string refreshToken)
    {
        return new AuthResponseDto
        {
            Token = await tokenService.CreateToken(user),
            RefreshToken = refreshToken,
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(2),
            SelfDto = ToSelfDto(user)
        };
    }
    public static MemberDetailDto ToDetailDto(this User user)
    {
        return new MemberDetailDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt,
            ModifiedAt = user.ModifiedAt,
            UploadedBriefings = user.UploadedBriefing?.Select(b => b.ToListDto()).ToList()
        };
    }
    public static MemberListDto ToListDto(this User user)
    {
        return new MemberListDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt,
            ModifiedAt = user.ModifiedAt
        };
    }
}