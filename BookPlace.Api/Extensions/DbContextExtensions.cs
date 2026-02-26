using System.Security.Claims;
using BookPlace.Core.Entities;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Api.Extensions;

public static class DbContextExtensions
{
    public static async Task<User?> GetLoggedInUser(this AppDbContext context,ClaimsPrincipal principal)
    {
        var userId = principal.GetUserId();
        return await context.Users
            .Include(u => u.UploadedBriefing)
            .Include(u => u.Comments)
            .Include(u => u.SavedBriefings)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}