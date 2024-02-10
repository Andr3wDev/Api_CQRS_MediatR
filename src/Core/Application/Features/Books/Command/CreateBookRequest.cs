using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;
using Application.Entity;

namespace Application.Features.Books.Command.CreateBook;

public class CreateBookRequest : IRequest<Guid>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime PublishDate { get; set; }
    public string? Publisher { get; set; }
    public int NumPages { get; set; }
    public Guid AuthorId { get; set; }
}

public class CreateBookRequestHandler : IRequestHandler<CreateBookRequest, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICustomMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookRequestHandler(
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        ICustomMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetById(request.AuthorId, cancellationToken);

        if (author is null)
        {
            throw new NotFoundException("Author not found");
        }

        var alreadyExists = await _bookRepository
            .CheckTitleExistsByAuthor(request.Title, request.AuthorId, cancellationToken);

        if (alreadyExists)
        {
            throw new ConflictException($"Book {request.Title} already exists");
        }

        var book = _mapper.Map<CreateBookRequest, Book>(request);

        var newBookId = await _bookRepository.Insert(book);

        // Check return bool for successful operation
        var success = await _unitOfWork.CommitAsync();

        return newBookId;
    }
}