using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using Domain.Dto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Books.Query.SearchBooks
{
    public class SearchBooksQuery : IRequest<IEnumerable<BookDto>>
    {
        public string? Title { get; set; }
        public SearchBooksQuery(string? title)
        {
            Title = title;
        }
    }

    public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICustomMapper _mapper;

        public SearchBooksQueryHandler(IBookRepository bookRepository, ICustomMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.SearchBooks(request.Title);

            var bookDtos = books.Select(b => _mapper.Map<Book, BookDto>(b));

            return bookDtos;
        }
    }
}
