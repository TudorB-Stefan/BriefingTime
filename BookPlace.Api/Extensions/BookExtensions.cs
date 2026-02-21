using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;

namespace BookPlace.Api.Extensions;

public static class BookExtensions
{
    public static BookListDto ToListDto(this Book book)
    {
        return new BookListDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author
        };
    }

    public static BookDetailDto ToDetailDto(this Book book)
    {
        return new BookDetailDto
        {
            Title = book.Title,
            Author = book.Author,
            Description = book.Description,
            FileSizeByte = book.FileSizeBytes,
            FileUrl = book.FileUrl,
            ContentType = book.ContentType,
            Reviews = book.Reviews.Select(r => r.ToDetailDto()).ToList()
        };
    }
}