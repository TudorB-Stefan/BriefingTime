using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class RegisterDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmaPassword { get; set; }
}