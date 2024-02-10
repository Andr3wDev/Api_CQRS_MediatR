using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Authors.Command.DeleteAuthor;

public class DeleteAuthorRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteAuthorRequest(Guid id) => Id = id;
}

public class DeleteAuthorRequestRequestHandler : IRequestHandler<DeleteAuthorRequest, Guid>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorRequestRequestHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteAuthorRequest request, CancellationToken cancellationToken)
    {
        var authorToDelete = await _authorRepository.GetById(request.Id, cancellationToken);

        if (authorToDelete is null)
        {
            throw new NotFoundException($"Author with id {request.Id} not found");
        }

        var deleteSuccess = await _authorRepository.Delete(authorToDelete);

        // Check return bool for successful operation
        var success = await _unitOfWork.CommitAsync();

        return request.Id;
    }
}


