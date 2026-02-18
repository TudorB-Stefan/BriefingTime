using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(string id);
    Task AddAsync(Category category);
    Task DeleteAsync(Category category);
}