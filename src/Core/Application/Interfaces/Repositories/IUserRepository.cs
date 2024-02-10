using Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid userId);
        Task<IEnumerable<User>> GetAll();
        Task<Guid> Insert(User newUser);
        Task<bool> Update(User updatedUser);
        Task<bool> Delete(User user);
    }
}
