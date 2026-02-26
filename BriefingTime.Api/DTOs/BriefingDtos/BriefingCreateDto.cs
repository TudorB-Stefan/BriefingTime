using System.ComponentModel.DataAnnotations;

namespace BriefingTime.Api.DTOs;

public class BriefingCreateDto
{
    [Required] public string Title { get; set; }
    public string? Description { get; set; }
    [Required] public string DepartmentId { get; set; }
    [Required] public IFormFile File { get; set; } = null!;
}