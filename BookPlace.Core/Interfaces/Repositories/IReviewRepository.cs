using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(string id);
    Task<IEnumerable<Review>> GetByBook(string bookId);
    Task<IEnumerable<Review>> GetByUser(string userId);
    Task AddAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(Review review);
}