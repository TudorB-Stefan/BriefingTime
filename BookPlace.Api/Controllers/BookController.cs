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
    [HttpPost("upload-book")]
    public async Task<ActionResult> UploadBook(BookCreateDto createBookDto)
    {
        if (createBookDto.File == null || createBookDto.File.Length == 0) return BadRequest("No file uploaded!");
        var allowedExtensions = new[] {".pdf",".enum"};
        
        string uploadFolder = Path.Combine(environment.WebRootPath,"uploads");
        if(!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);
        string fileId = Guid.NewGuid().ToString() + Path.GetExtension(createBookDto.File.FileName);
        string filePath = Path.Combine(uploadFolder,fileId);
        using(var fileStream = new FileStream(filePath, FileMode.Create))
            await createBookDto.File.CopyToAsync(fileStream);
        return Ok();
    }
    [HttpPut("edit-book-{id}")]
    public async Task<ActionResult> EditBook(BookUpdateDto bookUpdateDto)
    {
        if()
    }
}