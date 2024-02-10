using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Loans.Request.ReturnBook
{
    public class ReturnBookRequest : IRequest<Guid>
    {
        public Guid LoanId { get; set; }
    }

    public class ReturnBookRequestHandler : IRequestHandler<ReturnBookRequest, Guid>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReturnBookRequestHandler(
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(ReturnBookRequest request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetById(request.LoanId, cancellationToken);
            if (loan is null)
            {
                throw new NotFoundException("Loan not found");
            }

            // business logic for returning
            // TODO
            var success = await _unitOfWork.CommitAsync();

            return loan.Id;
        }
    }
}
