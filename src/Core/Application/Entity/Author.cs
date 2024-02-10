using Domain.Contracts;

namespace Application.Entity
{
    public class Author : AuditableEntity
    {
        public string? Name { get; set; }
        public virtual IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
