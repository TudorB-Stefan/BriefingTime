using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository
{
    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(string id)
    {
        return await context.Comments.FindAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetByBriefing(string briefingId)
    {
        return await context.Comments
            .Include(c => c.Briefing)
            .Where(c => c.BriefingId == briefingId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetByUser(string userId)
    {
        return await context.Comments
            .Include(c => c.User)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Comment comment)
    {
        context.Comments.Update(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment comment)
    {
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }
}