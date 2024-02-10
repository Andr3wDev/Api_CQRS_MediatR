using Application.Entity;

namespace Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetById(Guid bookId, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> GetByAuthor(Guid authorId, CancellationToken cancellationToken);
    Task<Guid> Insert(Book book);
    Task<bool> Delete(Book book);
    Task<bool> Update(Book updatedBook, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> SearchBooks(string title);
    Task<bool> CheckTitleExistsByAuthor(string title, Guid authorId, CancellationToken cancellationToken);
}