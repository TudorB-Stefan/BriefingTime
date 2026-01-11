namespace BookPlace.Api.DTOs;

public class CategoryDetailDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<BookListDto> Books { get; set; }
}