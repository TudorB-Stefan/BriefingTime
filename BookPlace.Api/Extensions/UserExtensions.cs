using System.Security.Claims;
using BookPlace.Api.DTOs;
using BookPlace.Api.DTOs.AuthDtos;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudMight.API.Extensions;

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
            UploadedBooks = user.UploadedBooks?.Select(b => new BookListDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
            }).ToList() ?? new List<BookListDto>(),
            DownloadLogs = user.DownloadLogs?.Select(dlog => new DownloadLogListDto
            {
                BookId = dlog.BookId,
                BookTitle = dlog.Book?.Title ?? "Unknown title",
                UserId = dlog.UserId,
                UserName = dlog.User?.UserName ?? "Unknown username",
                DownloadedAt = dlog.CreatedAt
            }).ToList() ?? new List<DownloadLogListDto>(),
            FavoriteBooks = user.FavoriteBooks?.Select(fbook => new FavoriteBookListDto
            {
                BookId = fbook.BookId,
                BookTitle = fbook.Book.Title,
                Author = fbook.Book.Author,
                CreatedAt = fbook.CreatedAt
            }).ToList() ?? new List<FavoriteBookListDto>(),
            Reviews = user.Reviews?.Select(r => new ReviewDetailDto
            {
                Id = r.Id,
                Grade = r.Grade,
                Title = r.Title,
                Description = r.Description,
                CreatedAt = r.CreatedAt,
                UserName = r.User?.UserName ?? "Unknown username",
                UserFirstName = r.User?.FirstName ?? "Unknown firstname",
                UserLastName = r.User?.LastName ?? "Unknown lastname"
            }).ToList() ?? new List<ReviewDetailDto>(),
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
}