using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;

namespace BookPlace.Api.Extensions;

public static class CategoryExtensions
{
    public static CategoryDetailDto ToDetailDto(this Category category)
    {
        return new CategoryDetailDto
        {
            Id = category.Id,
            Name = category.Name,
            Books = category.Books != null
                ? category.Books.Select(b => b.ToListDto()).ToList()
                : new List<BookListDto>()
        };
    }

    public static CategoryListDto ToListDto(this Category category)
    {
        return new CategoryListDto
        {
            Id = category.Id,
            Name = category.Name,
            Count = category.Books?.Count() ?? 0
        };
    }
}