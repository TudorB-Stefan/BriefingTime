namespace BookPlace.Api.DTOs;

public class ReviewUpdateDto
{
    public int Grade { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}