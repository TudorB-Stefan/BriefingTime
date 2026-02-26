using System.ComponentModel.DataAnnotations;

namespace BriefingTime.Api.DTOs;

public class DepartmentCreateDto
{
    [Required]public string Name { get; set; }
}