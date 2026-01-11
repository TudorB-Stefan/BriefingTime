using BookPlace.Api.DTOs;
using BookPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Api.Controllers;

public class ReviewController(AppDbContext context) : BaseController
{
    [HttpGet("get-review-{id}")]
    public async Task<ActionResult<ReviewDetailDto>> GetReviewDetailById(string id)
    {
        var reviewDto = await context.Reviews
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Select(r => new ReviewDetailDto
            {
                Id = r.Id,
                Grade = r.Grade,
                Title = r.Title,
                Description = r.Description,
                CreatedAt = r.CreatedAt,
                UserName = r.User.UserName ?? "unknow_user",
                UserFirstName = r.User.FirstName,
                UserLastName = r.User.LastName
            })
            .FirstOrDefaultAsync();
        if (reviewDto == null) return NotFound();
        return Ok(reviewDto);
    }
}