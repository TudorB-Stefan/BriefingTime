using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Repositories;

public class ReviewRepository(AppDbContext context) : IReviewRepository
{
    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await context.Reviews.ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(string id)
    {
        return await context.Reviews.FindAsync(id);
    }

    public async Task<IEnumerable<Review>> GetByBook(string bookId)
    {
        return await context.Reviews
            .Include(r => r.Book)
            .Where(r => r.BookId == bookId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByUser(string userId)
    {
        return await context.Reviews
            .Include(r => r.User)
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(Review review)
    {
        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Review review)
    {
        context.Reviews.Update(review);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Review review)
    {
        context.Reviews.Remove(review);
        await context.SaveChangesAsync();
    }
}