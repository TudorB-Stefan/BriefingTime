namespace BookPlace.Api.DTOs;

public class FavoriteBookListDto
{
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }
}