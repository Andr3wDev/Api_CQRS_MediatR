using Domain.Contracts;

namespace Application.Entity
{
    public class User : AuditableEntity
    {
        public string? Username { get; set; }
        public string? Email { get; set; }

    }
}
