using System.Security.Claims;
using BriefingTime.Core.Entities;
using BriefingTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Api.Extensions;

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