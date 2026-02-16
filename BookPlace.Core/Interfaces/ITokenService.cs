using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}