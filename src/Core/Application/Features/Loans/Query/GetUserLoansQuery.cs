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

namespace Application.Features.Loans.Query.GetUserLoans
{
    public class GetUserLoansQuery : IRequest<IEnumerable<LoanDto>>
    {
        public Guid UserId { get; set; }

        public GetUserLoansQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetUserLoansQueryHandler : IRequestHandler<GetUserLoansQuery, IEnumerable<LoanDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly ICustomMapper _mapper;

        public GetUserLoansQueryHandler(ILoanRepository loanRepository, ICustomMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanDto>> Handle(GetUserLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetUserLoans(request.UserId);
            var loanDtos = loans.Select(l => _mapper.Map<Loan, LoanDto>(l));
            return loanDtos;
        }
    }
}
