using Application.Interfaces.Repositories;
using Application.Abstractions;
using MediatR;

namespace Application.Features.Authors.Command.CreateAuthor;

public class CreateAuthorRequest : IRequest<Guid>
{
    public string Name { get; set; }
}

public class CreateAuthorRequestHandler : IRequestHandler<CreateAuthorRequest, Guid>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICustomMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorRequestHandler(
        IAuthorRepository authorRepository,
        ICustomMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<CreateAuthorRequest, Entity.Author>(request);

        var newAuthorId = await _authorRepository.Insert(author);

        // Check return bool for successful operation
        var success = await _unitOfWork.CommitAsync();

        return newAuthorId;
    }
}