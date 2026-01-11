namespace BookPlace.Core.Entities;

public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
}