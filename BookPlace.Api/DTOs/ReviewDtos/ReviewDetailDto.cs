namespace BookPlace.Api.DTOs;

public class ReviewDetailDto
{
    public string Id { get; set; }
    public int Grade { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
}