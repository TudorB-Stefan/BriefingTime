using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using BriefingTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Infrastructure.Repositories;

public class MemberRepository(AppDbContext context) : IMemberRepository
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await context.Users
            .Include(u => u.UploadedBriefing)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await context.Users
            .Include(u => u.UploadedBriefing)
            .FirstOrDefaultAsync(u => u.UserName == username);
    }
}