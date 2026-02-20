using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Repositories;

public class MemberRepository(AppDbContext context) : IMemberRepository
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}