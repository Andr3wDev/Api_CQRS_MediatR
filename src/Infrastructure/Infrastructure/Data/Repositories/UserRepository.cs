using Application.Entity;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public UserRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetById(Guid userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Guid> Insert(User newUser)
        {
            _dbContext.Users.Add(newUser);
            return newUser.Id;
        }

        public async Task<bool> Update(User updatedUser)
        {
            _dbContext.Users.Update(updatedUser);
            return true;
        }

        public async Task<bool> Delete(User user)
        {
            _dbContext.Users.Remove(user);
            return true;
        }
    }
}
