using BookPlace.Api.DTOs;
using BookPlace.Api.Extensions;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
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

    [HttpPost]
    public async Task<ActionResult> CreateFavBook(FavoriteBookCreateDto dto)
    {
        var userId = User.GetUserId();
        var book = await bookRepository.GetByIdAsync(dto.BookId);

        var favBook = new FavoriteBook
        {
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
            BookId = dto.BookId
        };
        await favoriteBookRepository.AddAsync(favBook);
        return Ok();
    }
}