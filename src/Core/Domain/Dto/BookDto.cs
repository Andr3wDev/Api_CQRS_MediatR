namespace Domain.Dto;

public class BookDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? PublishDate { get; set; }
    public int NumPages { get; set; }
    public string? Genre { get; set; }
    public string? Publisher { get; set; }
    public AuthorDto Author { get; set; }
    public IEnumerable<GenreDto> Genres { get; set; }
}