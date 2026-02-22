using BookPlace.Api.DTOs;
using BookPlace.Api.Extensions;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Api.Controllers;

public class ReviewController(IReviewRepository reviewRepository,IBookRepository bookRepository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDetailDto>>> GetAll()
    {
        var reviews = await reviewRepository.GetAllAsync();
        var reviewDtos = reviews.Select(r => r.ToDetailDto()).ToList();
        return Ok(reviewDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDetailDto>> GetById(string id)
    {
        var review = await reviewRepository.GetByIdAsync(id);
        if (review == null) return NotFound();
        var reviewDto = review.ToDetailDto();
        return Ok(reviewDto);
    }

    [HttpGet("review-book-{bookId}")]
    public async Task<ActionResult<IEnumerable<ReviewDetailDto>>> GetByBook(string bookId)
    {
        var reviews = await reviewRepository.GetByBook(bookId);
        var reviewDto = reviews.Select(r => r.ToDetailDto()).ToList();
        return Ok(reviewDto);
    }
    
    [HttpGet("review-user-{userId}")]
    public async Task<ActionResult<IEnumerable<ReviewDetailDto>>> GetByUser(string userId)
    {
        var reviews = await reviewRepository.GetByUser(userId);
        var reviewDto = reviews.Select(r => r.ToDetailDto()).ToList();
        return Ok(reviewDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateReview(ReviewCreateDto dto)
    {
        var book = await bookRepository.GetByIdAsync(dto.BookId);
        var userId = User.GetUserId();
        if (book == null) return NotFound("Book not found.");
        var review = new Review
        {
            Id = Guid.NewGuid().ToString(),
            Grade = dto.Grade,
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            UserId = userId,
            BookId = dto.BookId
        };
        await reviewRepository.AddAsync(review);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult> UpdateReview(string id,ReviewUpdateDto dto)
    {
        var userId = User.GetUserId();
        if (userId == null) return NotFound("User not found");
        var review = await reviewRepository.GetByIdAsync(id);
        if(review == null) return NotFound("Review not found");
        if (review.UserId != userId) return Forbid("You can only edit your own reviews.");
        review.Description = dto.Description;
        review.Title = dto.Title;
        review.Grade = dto.Grade;
        await reviewRepository.UpdateAsync(review);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteReview(string id)
    {
        var review = await reviewRepository.GetByIdAsync(id);
        if (review == null) return NotFound();
        if (review.UserId != User.GetUserId()) return Forbid("You can only edit your own reviews.");
        await reviewRepository.DeleteAsync(review);
        return Ok();
    }
}