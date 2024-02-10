using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using Domain.Dto;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Books.Query.GetBookById;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public Guid Id { get; set; }

    public GetBookByIdQuery(Guid id) => Id = id;
}

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICustomMapper _mapper;

    public GetBookByIdQueryHandler(IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        ICustomMapper mapper)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var getBook = await _bookRepository.GetById(request.Id, cancellationToken);
        if (getBook is null)
        {
            throw new NotFoundException("Book not found");
        }

        var getAuthor = await _authorRepository.GetById(getBook.AuthorId, cancellationToken);
        if (getAuthor is null)
        {
            throw new NotFoundException($"Author not found for book with id {request.Id}");
        }

        var bookDto = _mapper.Map<Book, BookDto>(getBook);

        var authDto = _mapper.Map<Author, AuthorDto>(getAuthor!);
        bookDto.Author = authDto;

        var genreDtos = getBook.Genres.Select(genre => _mapper.Map<Genre, GenreDto>(genre));
        bookDto.Genres = genreDtos;

        return bookDto;
    }
}