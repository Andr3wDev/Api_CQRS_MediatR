using Application.Entity;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _dbContext;

        public LoanRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Loan>> GetBookLoans(Guid bookId)
        {
            return await _dbContext.Loans
                .Where(loan => loan.BookId == bookId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetUserLoans(Guid userId)
        {
            return await _dbContext.Loans
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }
        public async Task<Loan?> GetById(Guid loanId, CancellationToken cancellationToken)
        {
            return await _dbContext.Loans.FindAsync(loanId);
        }

        public async Task<IEnumerable<Loan>> GetAll()
        {
            return await _dbContext.Loans.ToListAsync();
        }

        public async Task<Guid> Insert(Loan newLoan)
        {
            _dbContext.Loans.Add(newLoan);
            return newLoan.Id;
        }

        public async Task<bool> Update(Loan updatedLoan)
        {
            _dbContext.Loans.Update(updatedLoan);
            return true;
        }

        public async Task<bool> Delete(Loan loan)
        {
            _dbContext.Loans.Remove(loan);
            return true;
        }
    }
}