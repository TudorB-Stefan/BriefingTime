using Microsoft.AspNetCore.Components.Web;

namespace BookPlace.Api.DTOs;

public class BookListDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public double AverageGrade { get; set; }
}