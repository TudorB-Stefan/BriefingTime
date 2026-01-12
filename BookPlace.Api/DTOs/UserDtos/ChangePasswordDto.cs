using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class ChangePasswordDto
{
    [Required]
    public string OldPassword { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmaPassword { get; set; }
}