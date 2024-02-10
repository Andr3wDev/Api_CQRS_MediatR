using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Books.Command.DeleteBook;

public class DeleteBookRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteBookRequest(Guid id) => Id = id;
}

public class DeleteBookRequestHandler : IRequestHandler<DeleteBookRequest, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookRequestHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        var bookToDelete = await _bookRepository.GetById(request.Id, cancellationToken);

        if (bookToDelete is null)
        {
            throw new NotFoundException($"Book with id {request.Id} not found");
        }

        var deleteSuccess = await _bookRepository.Delete(bookToDelete);

        // Check return bool for successful operation
        var success = await _unitOfWork.CommitAsync();

        return request.Id;
    }
}