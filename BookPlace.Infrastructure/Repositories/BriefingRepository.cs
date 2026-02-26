using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Repositories;

public class BriefingRepository(AppDbContext context) : IBriefingRepository
{
    public async Task<IEnumerable<Briefing>> GetAllAsync()
    {
        return await context.Briefings
            .Where(b => b.ExpiresAt > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<Briefing?> GetByIdAsync(string id)
    {
        return await context.Briefings.FindAsync(id);
    }

    public async Task<Briefing?> GetByIdWithDetailsAsync(string id)
    {
        return await context.Briefings
            .Include(b => b.Comments)
            .Include(b => b.SavedBriefings)
            .Include(b => b.Department)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddAsync(Briefing briefing)
    {
        await context.Briefings.AddAsync(briefing);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Briefing briefing)
    {
        context.Briefings.Update(briefing);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Briefing briefing)
    {
        context.Remove(briefing);
        await context.SaveChangesAsync();
    }
}