namespace BookPlace.Api.DTOs;

public class MemberDetailDto
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public ICollection<BookListDto> UploadedBooks { get; set; }
}