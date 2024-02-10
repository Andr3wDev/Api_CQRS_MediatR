using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Application.Entity;

namespace Infrastructure.Data.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _dbContext;
    public AuthorRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await _dbContext.Authors
            .Include(author => author.Books).
            ToListAsync();
    }

    public async Task<Author?> GetById(Guid authorId, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors
            .Include(author => author.Books)
            .FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);

        return author != null ? author : null;
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _dbContext.Authors.AnyAsync(author => author.Id == id);
    }

    public async Task<Guid> Insert(Author author)
    {
        _dbContext.Authors.Add(author);
        return author.Id;
    }

    public async Task<bool> Update(Author updatedAuthor, CancellationToken cancellationToken)
    {
        _dbContext.Authors.Update(updatedAuthor);
        return true;
    }

    public async Task<bool> Delete(Author author)
    {
        _dbContext.Authors.Remove(author);
        return true;
    }
}