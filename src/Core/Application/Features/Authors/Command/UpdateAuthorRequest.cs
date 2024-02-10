using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Authors.Command.UpdateAuthor;

public class UpdateAuthorRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class UpdateAuthorRequestHandler : IRequestHandler<UpdateAuthorRequest, Guid>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorRequestHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateAuthorRequest request, CancellationToken cancellationToken)
    {
        var existingAuthor = await _authorRepository.GetById(request.Id, cancellationToken);

        if (existingAuthor is null) 
        {
            throw new NotFoundException($"Author with id {request.Id} not found.");
        }

        existingAuthor.Name = request.Name;

        await _authorRepository.Update(existingAuthor, cancellationToken);

        // Check return bool for successful operation
        var success = await _unitOfWork.CommitAsync();

        return request.Id;
    }
}