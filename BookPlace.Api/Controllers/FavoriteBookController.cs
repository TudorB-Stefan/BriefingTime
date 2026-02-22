using BookPlace.Api.DTOs;
using BookPlace.Api.Extensions;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookPlace.Api.Controllers;

public class FavoriteBookController(IFavoriteBookRepository favoriteBookRepository,IBookRepository bookRepository) : BaseController
{
    [HttpGet("user-{userId}")]
    public async Task<ActionResult<IEnumerable<FavoriteBookListDto>>> GetByUserId(string userId)
    {
        var favBooks = await favoriteBookRepository.GetFavsByUserAsync(userId);
        var favBooksDto = favBooks.Select(f => f.ToListDto()).ToList();
        return Ok(favBooksDto);
    }
    [HttpGet("book-{bookId}")]
    public async Task<ActionResult<IEnumerable<FavoriteBookListDto>>> GetByBookId(string bookId)
    {
        var favBooks = await favoriteBookRepository.GetFavsByBookAsync(bookId);
        var favBooksDto = favBooks.Select(f => f.ToListDto()).ToList();
        return Ok(favBooksDto);
    }

    [HttpGet("{userId}/{bookId}")]
    public async Task<ActionResult<FavoriteBookListDto>> GetByIds(string userId, string bookId)
    {
        var favBook = await favoriteBookRepository.GetByIdAsync(userId, bookId);
        if (favBook == null) return NotFound();
        var favBookDto = favBook.ToListDto();
        return Ok(favBookDto);
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult> CreateFavBook(FavoriteBookCreateDto dto)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        
        var book = await bookRepository.GetByIdAsync(dto.BookId);
        if (book == null) return NotFound();

        var existingFav = await favoriteBookRepository.GetByIdAsync(userId, dto.BookId);
        if (existingFav != null) return BadRequest("You already favorited this book.");

        var favBook = new FavoriteBook
        {
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
            BookId = dto.BookId
        };
        await favoriteBookRepository.AddAsync(favBook);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{bookId}")]
    public async Task<ActionResult> DeleteFavBook(string bookId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var book = await favoriteBookRepository.GetByIdAsync(userId,bookId);
        if (book == null) return NotFound();
        await favoriteBookRepository.DeleteAsync(book);
        return Ok();
    }
}