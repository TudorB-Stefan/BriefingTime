namespace BookPlace.Api.DTOs.AuthDtos;

public class AuthResponseDto
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public SelfDto? SelfDto { get; set; }
}