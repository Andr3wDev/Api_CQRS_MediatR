using Application.Abstractions;
using Application.Interfaces.Repositories;
using Domain.Dto;
using MediatR;

namespace Application.Features.Authors.Query.GetAllAuthors;

public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
{
    public GetAllAuthorsQuery() { }
}

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICustomMapper _mapper;

    public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository, ICustomMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var getAuthors = await _authorRepository.GetAll();

        var response = getAuthors.Select(b => _mapper.Map<Entity.Author, AuthorDto>(b));

        return response;
    }
}