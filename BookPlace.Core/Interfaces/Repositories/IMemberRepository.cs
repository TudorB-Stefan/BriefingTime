using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces.Repositories;

public interface IMemberRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByUsername(string username);
}