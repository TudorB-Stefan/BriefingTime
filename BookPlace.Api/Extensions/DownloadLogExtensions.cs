using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;

namespace BookPlace.Api.Extensions;

public static class DownloadLogExtensions
{
    public static DownloadLogDetailDto ToDetailDto(this DownloadLog downloadLog)
    {
        return new DownloadLogDetailDto
        {
            UserId = downloadLog.UserId,
            BookId = downloadLog.BookId
        };
    }
}