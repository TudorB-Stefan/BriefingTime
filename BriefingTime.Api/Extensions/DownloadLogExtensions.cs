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
}