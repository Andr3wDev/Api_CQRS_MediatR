using Domain.Contracts;

namespace Application.Entity
{
    public class Genre : AuditableEntity
    {
        public string? Name { get; set; }
    }
}
