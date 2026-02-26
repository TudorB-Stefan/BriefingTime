using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Repositories;

public class SavedBriefingRepository(AppDbContext context) : ISavedBriefingRepository
{
    public async Task<IEnumerable<SavedBriefing>> GetByUserAsync(string userId)
    {
        return await context.SavedBriefings
            .Include(s => s.Briefing).ThenInclude(b => b.UserId)
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<SavedBriefing>> GetByBriefingAsync(string briefingId)
    {
        return await context.SavedBriefings
            .Include(s => s.Briefing)
            .Where(s => s.UserId == briefingId)
            .ToListAsync();
    }

    public async Task<SavedBriefing?> GetByIdAsync(string userId, string briefingId)
    {
        return await context.SavedBriefings.FirstOrDefaultAsync(f => f.UserId == userId && f.BriefingId == briefingId);
    }

    public async Task AddAsync(SavedBriefing savedBriefing)
    {
        await context.SavedBriefings.AddAsync(savedBriefing);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SavedBriefing savedBriefing)
    {
        context.SavedBriefings.Remove(savedBriefing);
        await context.SaveChangesAsync();
    }
}