using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class BookCreateDto
{
    [Required] public string Title { get; set; }
    [Required] public string Author { get; set; }
    public string? Description { get; set; }
    [Required] public string CategoryId { get; set; }
    [Required] public IFormFile File { get; set; } = null!;
    [Required] public string Publisher { get; set; }
}