namespace BookPlace.Api.DTOs;

public class FavoriteBookListDto
{
    public string BookId { get; set; }
    public string UserId { get; set; }
    public string BookTitle { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }
}