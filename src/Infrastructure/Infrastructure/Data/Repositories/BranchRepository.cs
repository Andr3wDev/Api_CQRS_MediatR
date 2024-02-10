using Application.Entity;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BranchRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Branch?> GetById(Guid branchId)
        {
            return await _dbContext.Branches.FindAsync(branchId);
        }

        public async Task<IEnumerable<Branch>> GetAll()
        {
            return await _dbContext.Branches.ToListAsync();
        }

        public async Task<Guid> Insert(Branch newBranch)
        {
            _dbContext.Branches.Add(newBranch);
            return newBranch.Id;
        }

        public async Task<bool> Update(Branch updatedBranch)
        {
            _dbContext.Branches.Update(updatedBranch);
            return true;
        }

        public async Task<bool> Delete(Branch branch)
        {
            _dbContext.Branches.Remove(branch);
            return true;
        }
    }
}
