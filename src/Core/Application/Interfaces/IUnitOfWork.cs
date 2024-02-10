using System.Data;

namespace Application.Abstractions;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
}