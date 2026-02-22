namespace BookPlace.Api.DTOs.MemberDtos;

public class MemberDetailDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public ICollection<BookListDto> UploadedBooks { get; set; }
}