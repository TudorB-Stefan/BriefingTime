namespace BookPlace.Api.DTOs;

public class ReviewCreateDto
{
    public int Grade { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    //user handeled by jwt token
    public string BookId { get; set; }
}