using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Loans.Request.BorrowBook
{
    public class BorrowBookRequest : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }

    public class BorrowBookRequestHandler : IRequestHandler<BorrowBookRequest, Guid>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICustomMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BorrowBookRequestHandler(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            ICustomMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(BorrowBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetById(request.BookId, cancellationToken);
            if (book is null)
            {
                throw new NotFoundException("Book not found");
            }

            if (!book.IsAvailableForLoan)
            {
                throw new ConflictException("Book is not available for loan");
            }

            // business logic for borrowing ..
            // TODO
            var success = await _unitOfWork.CommitAsync();

            return Guid.NewGuid();
        }
    }
}
