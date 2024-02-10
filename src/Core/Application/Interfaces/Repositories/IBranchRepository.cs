using Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch?> GetById(Guid branchId);
        Task<IEnumerable<Branch>> GetAll();
        Task<Guid> Insert(Branch newBranch);
        Task<bool> Update(Branch updatedBranch);
        Task<bool> Delete(Branch branch);
    }
}
