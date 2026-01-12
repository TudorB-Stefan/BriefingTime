using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;
using BookPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Api.Controllers;

public class BookController(AppDbContext context, IWebHostEnvironment environment) : BaseController
{
    [HttpGet("get-all-books")]
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
        return Ok(allBooks); 
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetailDto>> GetBookDetailById(string id)
    {
        var bookDto = await context.Books
            .AsNoTracking()
            .Where(b => b.Id == id)
            .Select(b => new BookDetailDto
            {
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                FileSizeByte = b.FileSizeBytes,
                FileUrl = b.FileUrl,
                ContentType = b.ContentType,
                Reviews = b.Reviews.Select(r => new ReviewDetailDto
                {
                    Id = r.Id,
                    Grade = r.Grade,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,
                    UserName = r.User.UserName ?? "unknown_user",
                    UserFirstName = r.User.FirstName,
                    UserLastName = r.User.LastName
                }).ToList()
            })
            .FirstOrDefaultAsync();
        if (bookDto == null) return NotFound();
        return Ok(bookDto);
    }
}