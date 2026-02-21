using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;

namespace BookPlace.Api.Extensions;

public static class FavoriteBookExtensions
{
    public static FavoriteBookListDto ToListDto(this FavoriteBook favoriteBookListDto)
    {
        return new FavoriteBookListDto
        {
            BookId = favoriteBookListDto.BookId,
            UserId = favoriteBookListDto.UserId,
            BookTitle = favoriteBookListDto.Book.Title,
            Author = favoriteBookListDto.Book.Author,
            CreatedAt = favoriteBookListDto.CreatedAt
        };
    }
}