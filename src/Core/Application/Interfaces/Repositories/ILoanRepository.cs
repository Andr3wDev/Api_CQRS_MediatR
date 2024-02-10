using Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetBookLoans(Guid bookId);
        Task<IEnumerable<Loan>> GetUserLoans(Guid userId);
        Task<Loan?> GetById(Guid loanId, CancellationToken cancellationToken);
        Task<IEnumerable<Loan>> GetAll();
        Task<Guid> Insert(Loan newLoan);
        Task<bool> Update(Loan updatedLoan);
        Task<bool> Delete(Loan loan);
    }
}
