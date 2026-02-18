using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces.Repositories;

public interface IFavoriteBookRepository
{
    Task<IEnumerable<FavoriteBook>> GetFavsByUserAsync(string userId);
    Task<IEnumerable<FavoriteBook>> GetFavsByBookAsync(string bookId);
    Task<FavoriteBook?> GetByIdAsync(string userId,string bookId);
    Task AddAsync(FavoriteBook favoriteBook);
    Task DeleteAsync(FavoriteBook favoriteBook);
}