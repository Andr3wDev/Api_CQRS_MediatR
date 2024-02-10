using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using Domain.Dto;
using MediatR;

namespace Application.Features.Books.Query.GetAllBooks;

public class GetAllBooksQuery : IRequest<IEnumerable<BookDto>>
{
    public GetAllBooksQuery() { }
}

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly ICustomMapper _mapper;

    public GetAllBooksQueryHandler(IBookRepository bookRepository, ICustomMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var getBooks = await _bookRepository.GetAll();

        var response = getBooks.Select(b => _mapper.Map<Book, BookDto>(b));

        return response;
    }
}