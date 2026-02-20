using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Repositories;

public class FavoriteBookRepository(AppDbContext context) : IFavoriteBookRepository
{
    public async Task<IEnumerable<FavoriteBook>> GetFavsByUserAsync(string userId)
    {
        return await context.FavoriteBooks
            .Include(f => f.Book).ThenInclude(b => b.UserId)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<FavoriteBook>> GetFavsByBookAsync(string bookId)
    {
        return await context.FavoriteBooks
            .Include(f => f.Book)
            .Where(f => f.UserId == bookId)
            .ToListAsync();
    }

    public async Task<FavoriteBook?> GetByIdAsync(string userId, string bookId)
    {
        return await context.FavoriteBooks.FirstOrDefaultAsync(f => f.UserId == userId && f.BookId == bookId);
    }

    public async Task AddAsync(FavoriteBook favoriteBook)
    {
        await context.FavoriteBooks.AddAsync(favoriteBook);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(FavoriteBook favoriteBook)
    {
        context.FavoriteBooks.Remove(favoriteBook);
        await context.SaveChangesAsync();
    }
}