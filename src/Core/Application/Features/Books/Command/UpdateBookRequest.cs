using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Books.Command.UpdateBook;

public class UpdateBookRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime PublishDate { get; set; }
    public string? Publisher { get; set; }
    public int NumPages { get; set; }
    public Guid AuthorId { get; set; }
}

public class UpdateBookRequestHandler : IRequestHandler<UpdateBookRequest, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookRequestHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var existingBook = await _bookRepository.GetById(request.Id, cancellationToken);

        if (existingBook is null) 
        {
            throw new NotFoundException($"Book with id {request.Id} not found.");
        }

        existingBook.Title = request.Title;
        existingBook.Description = request.Description;
        existingBook.PublishDate = request.PublishDate;
        existingBook.Publisher = request.Publisher;
        existingBook.NumPages = request.NumPages;        

        await _bookRepository.Update(existingBook, cancellationToken);

        // Check return bool for successful operation
        var success = await _unitOfWork.CommitAsync();

        return request.Id;
    }
}