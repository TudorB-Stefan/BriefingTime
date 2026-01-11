using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class CategoryCreateDto
{
    [Required]public string Name { get; set; }
}