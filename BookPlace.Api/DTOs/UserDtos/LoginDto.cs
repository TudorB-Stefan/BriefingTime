using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class LoginDto
{
    [Required]public string Email { get; set; }
    [Required]public string Password { get; set; }
}