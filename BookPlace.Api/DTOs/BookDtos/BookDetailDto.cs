namespace BookPlace.Api.DTOs;

public class BookDetailDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public long FileSizeByte { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string ContentType { get; set; } = "application/pdf";
    public ICollection<ReviewDetailDto> Reviews { get; set; }
}