using Domain.Contracts;

namespace Application.Entity
{
    public class Branch : AuditableEntity
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
    }
}
