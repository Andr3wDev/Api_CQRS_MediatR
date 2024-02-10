using Application.Abstractions;
using Application.Interfaces.Repositories;
using Domain.Dto;
using Application.Exceptions;
using MediatR;
using Application.Entity;

namespace Application.Features.Authors.Query.GetAuthorById;

public class GetAuthorByIdQuery : IRequest<AuthorDto>
{
    public Guid Id { get; set; }

    public GetAuthorByIdQuery(Guid id) => Id = id;
}

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICustomMapper _mapper;

    public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository,
        IBookRepository bookRepository,
        ICustomMapper mapper)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var getAuthor = await _authorRepository.GetById(request.Id, cancellationToken);

        if (getAuthor is null)
        {
            throw new NotFoundException($"Author with id {request.Id} not found.");
        }

        var authorBooks = await _bookRepository.GetByAuthor(request.Id, cancellationToken);

        var authorDto = _mapper.Map<Entity.Author, AuthorDto>(getAuthor);
        authorDto.Books = _mapper.Map<IEnumerable<Book>, List<BookDto>>(authorBooks);

        return authorDto;
    }
}