using Application.Abstractions;
using Infrastructure.Data.Context;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly LibraryDbContext _dbContext;

    public UnitOfWork(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}