using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;

namespace BriefingTime.Api.Extensions;

public static class DownloadLogExtensions
{
    public static DownloadLogDetailDto ToDetailDto(this DownloadLog downloadLog)
    {
        return new DownloadLogDetailDto
        {
            UserId = downloadLog.UserId,
            BriefingId = downloadLog.BriefingId
        };
    }
    public static DownloadLogListDto ToListDto(this DownloadLog downloadLog)
    {
        return new DownloadLogListDto
        {
            UserId = downloadLog.UserId,
            UserName = downloadLog.User?.UserName ?? "Unknown Username",
            BriefingId = downloadLog.BriefingId,
            BriefingTitle = downloadLog.Briefing?.Title ?? "Deleted Briefing",
            DownloadedAt = downloadLog.CreatedAt
        };
    }
}