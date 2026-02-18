using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(string id);
    Task<Book?> GetByIdWithDetailsAsync(string id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
}