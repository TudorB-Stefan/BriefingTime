using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class ChangePasswordDto
{
    [Required]public string OldPassword { get; set; }
    [Required]public string Password { get; set; }
    [Required]public string ConfirmaPassword { get; set; }
}