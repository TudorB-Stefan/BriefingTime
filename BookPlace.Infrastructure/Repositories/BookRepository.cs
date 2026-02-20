using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Repositories;

public class BookRepository(AppDbContext context) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await context.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(string id)
    {
        return await context.Books.FindAsync(id);
    }

    public async Task<Book?> GetByIdWithDetailsAsync(string id)
    {
        return await context.Books
            .Include(b => b.Reviews)
            .Include(b => b.FavoriteUsers)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddAsync(Book book)
    {
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        context.Remove(book);
        await context.SaveChangesAsync();
    }
}