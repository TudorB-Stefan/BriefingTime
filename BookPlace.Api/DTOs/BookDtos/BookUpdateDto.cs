using System.ComponentModel.DataAnnotations;

namespace BookPlace.Api.DTOs;

public class BookUpdateDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Description { get; set; }
    public string? CategoryId { get; set; }
    public string? Publisher { get; set; }
}