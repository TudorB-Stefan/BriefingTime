using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(string id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category category)
    {
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }
}