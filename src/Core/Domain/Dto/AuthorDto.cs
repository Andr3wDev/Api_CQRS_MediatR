namespace Domain.Dto;

public class AuthorDto
{
    public string Name { get; set; }
    public virtual IList<BookDto> Books { get; set; }
}