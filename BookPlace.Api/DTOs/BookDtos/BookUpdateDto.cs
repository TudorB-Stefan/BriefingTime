using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class BookUpdateDto
{
    [Required] public string Title { get; set; }
    [Required] public string Author { get; set; }
    public string? Description { get; set; }
    [Required] public int CategoryId { get; set; }
    [Required] public string Publisher { get; set; }
}