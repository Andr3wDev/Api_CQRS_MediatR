using Domain.Contracts;

namespace Application.Entity
{
    public class Reservation : AuditableEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }
        public DateTime ReservationDate { get; set; } // Reservation made
        public DateTime StartDate { get; set; }       // Reservation starts
    }
}
