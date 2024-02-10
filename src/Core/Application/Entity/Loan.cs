using Domain.Contracts;

namespace Application.Entity
{
    public class Loan : AuditableEntity
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
