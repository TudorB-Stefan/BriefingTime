namespace BookPlace.Api.DTOs;

public class AuthResponseDto
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public SelfDto? SelfDto { get; set; }
}