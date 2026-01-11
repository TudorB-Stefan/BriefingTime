using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;
using BookPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Api.Controllers;

public class BookController(AppDbContext context, IWebHostEnvironment environment) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookListDto>>> GetBookList()
    {
        var allBooks = await context.Books
            .AsNoTracking()
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new BookListDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
            })
            .ToListAsync();
        return allBooks;
    }
}