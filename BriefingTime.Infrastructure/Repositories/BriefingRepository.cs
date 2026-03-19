using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using BriefingTime.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Infrastructure.Repositories;

public class BriefingRepository(AppDbContext context) : IBriefingRepository
{
    public async Task<IEnumerable<Briefing>> GetAllAsync()
    {
        return await context.Briefings
            .Where(b => b.ExpiresAt > DateTime.UtcNow)
            .Include(b => b.Department)
            .ToListAsync();
    }

    public async Task<IEnumerable<Briefing>> GetAllForUserAsync(string userId)
    {
        return await context.Briefings
            .Where(b => b.ExpiresAt > DateTime.UtcNow)
            .Where(b => context.UserDepartments.Any(ud => ud.UserId == userId && ud.DepartmentId == b.DepartmentId
            ))
            .Include(b => b.Department)
            .ToListAsync();
    }

    public async Task<Briefing?> GetByIdAsync(string id)
    {
        return await context.Briefings
            .Include(b => b.Department)
            .FirstOrDefaultAsync(b=> b.Id == id);
    }

    public async Task<Briefing?> GetByIdAsyncForUser(string userId, string briefId)
    {
        return await context.Briefings
            .Include(b => b.Department)
            .FirstOrDefaultAsync(b=> 
                b.Id == briefId &&
                context.UserDepartments.Any(ud => 
                    ud.DepartmentId == b.DepartmentId && ud.UserId == userId
                )
            );
    }

    public async Task<Briefing?> GetByIdWithDetailsAsync(string userId,string briefId)
    {
        return await context.Briefings
            .Include(b => b.Comments)
            .Include(b => b.SavedBriefings)
            .Include(b => b.Department)
            .FirstOrDefaultAsync(b => 
                b.Id == briefId && 
                context.UserDepartments.Any(ud => 
                    ud.DepartmentId == b.DepartmentId && ud.UserId == userId
            ));
    }

    public async Task<IEnumerable<Briefing>> GetByUserId(string userId)
    {
        return await context.Briefings
            .Where(b => b.UserId == userId)
            .Include(b => b.Department)
            .ToListAsync();
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

    public async Task<IEnumerable<Briefing>> GetOldBriefings()
    {
        var oldBriefs = await context.Briefings
            .Where(b => b.CreatedAt.AddHours(24) < DateTime.UtcNow)
            .ToListAsync();
        return oldBriefs;
    }
}