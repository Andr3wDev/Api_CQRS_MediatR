using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using Domain.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Loans.Query.GetBookLoans
{
    public class GetBookLoansQuery : IRequest<IEnumerable<LoanDto>>
    {
        public Guid BookId { get; set; }

        public GetBookLoansQuery(Guid bookId)
        {
            BookId = bookId;
        }
    }

    public class GetBookLoansQueryHandler : IRequestHandler<GetBookLoansQuery, IEnumerable<LoanDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ICustomMapper _mapper;

        public GetBookLoansQueryHandler(ILoanRepository loanRepository, ICustomMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> Handle(GetBookLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetBookLoans(request.BookId);
            var loanDtos = loans.Select(l => _mapper.Map<Loan, LoanDto>(l));
            return loanDtos;
        }
    }
}
