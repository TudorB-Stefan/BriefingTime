using System.Security.Claims;
using BookPlace.Core.Entities;
using BookPlace.Infrastructure.Data;
using CloudMight.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Api.Extensions;

public static class DbContextExtensions
{
    public static async Task<User?> GetLoggedInUser(this AppDbContext context,ClaimsPrincipal principal)
    {
        var userId = principal.GetUserId();
        return await context.Users
            .Include(u => u.UploadedBooks)
            .Include(u => u.Reviews)
            .Include(u => u.FavoriteBooks)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}