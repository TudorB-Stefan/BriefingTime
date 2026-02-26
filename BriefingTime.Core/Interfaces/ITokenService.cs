using BriefingTime.Core.Entities;

namespace BriefingTime.Core.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
    string GenerateRefreshToken();
}