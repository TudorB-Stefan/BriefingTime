using System.ComponentModel.DataAnnotations;

namespace BriefingTime.Api.DTOs;

public class BriefingUpdateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DepartmentId { get; set; }
}