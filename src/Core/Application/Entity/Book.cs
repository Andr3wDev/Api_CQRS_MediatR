using Domain.Contracts;

namespace Application.Entity
{
    public class Book : AuditableEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Publisher { get; set; }
        public int NumPages { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public bool IsAvailableForLoan { get; set; }
    }
}