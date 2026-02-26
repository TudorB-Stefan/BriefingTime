using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class CommentCreateDto
{
    [Required]public string Title { get; set; }
    [Required]public string Description { get; set; }
    [Required]public string BriefingId { get; set; }
}