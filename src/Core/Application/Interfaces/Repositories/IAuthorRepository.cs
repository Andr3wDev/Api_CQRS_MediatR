using Application.Entity;

namespace Application.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAll();
        Task<bool> Exists(Guid id);
        Task<Author?> GetById(Guid authorId, CancellationToken cancellationToken);
        Task<Guid> Insert(Author author);
        Task<bool> Update(Author updatedAuthor, CancellationToken cancellationToken);
        Task<bool> Delete(Author author);
    }
}
