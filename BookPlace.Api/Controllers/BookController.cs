using BookPlace.Api.DTOs;
using BookPlace.Api.Extensions;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Api.Controllers;

public class BookController(IWebHostEnvironment environment,IBookRepository bookRepository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookListDto>>> GetBookList()
    {
        var books = await bookRepository.GetAllAsync();
        var booksDto = books.Select(b => b.ToListDto());
        return Ok(booksDto); 
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetailDto>> GetBookDetailById(string id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book == null) return NotFound();
        var bookDto = book.ToDetailDto();
        return Ok(bookDto);
    }

    [Authorize]
    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadBook(string id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book == null) return NotFound();
        if (string.IsNullOrEmpty(book.FileUrl)) return NotFound("Invalid book file path.");
        string filePath = Path.Combine(environment.WebRootPath, "uploads", book.FileUrl);
        if(!System.IO.File.Exists(filePath)) return NotFound("Corrupted book file.");
        string contentType = book.ContentType ?? "application/octet-stream";
        string downloadName = book.OriginalFileName ?? book.FileUrl;
        return PhysicalFile(filePath, contentType, downloadName);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UploadBook([FromForm]BookCreateDto dto)
    {
        if (dto.File == null || dto.File.Length == 0) return BadRequest("No file uploaded!");
        var allowedExtensions = new[] {".pdf",".epub"};
        var extension = Path.GetExtension(dto.File.FileName).ToLower();
        if (!allowedExtensions.Contains(extension)) return BadRequest("Invalid file extension!");
        
        string uploadFolder = Path.Combine(environment.WebRootPath,"uploads");
        if(!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);
        
        string fileId = Guid.NewGuid().ToString() + Path.GetExtension(dto.File.FileName);
        string filePath = Path.Combine(uploadFolder,fileId);
        
        using(var fileStream = new FileStream(filePath, FileMode.Create))
            await dto.File.CopyToAsync(fileStream);
        var book = new Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Author = dto.Author,
            Description = dto.Description,
            Publisher = dto.Publisher,
            FileUrl = fileId,
            OriginalFileName = dto.File.FileName,
            FileSizeBytes = dto.File.Length,
            ContentType = dto.File.ContentType,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            CategoryId = dto.CategoryId,
            UserId = User.GetUserId(),
        };
        await bookRepository.AddAsync(book);
        return Ok();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBook(string id,BookUpdateDto dto)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book == null) return NotFound();
        if (book.UserId != User.GetUserId()) return Forbid("You can only delete your own books.");

        book.Title = dto.Title ?? book.Title;
        book.Author = dto.Author ?? book.Author;
        book.Description = dto.Description ?? book.Description;
        book.CategoryId = dto.CategoryId ?? book.CategoryId;
        book.Publisher = dto.Publisher ?? book.Publisher;
        book.ModifiedAt = DateTime.UtcNow;
        
        await bookRepository.UpdateAsync(book);
        return Ok();
    }
    
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteBook(string id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book == null) return NotFound();
        if (book.UserId != User.GetUserId()) return Forbid("You can only delete your own books.");
        
        await bookRepository.DeleteAsync(book);
        return Ok();
    }
}