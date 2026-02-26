using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class DepartmentCreateDto
{
    [Required]public string Name { get; set; }
}