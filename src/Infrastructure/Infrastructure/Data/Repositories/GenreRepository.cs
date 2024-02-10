using Application.Entity;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryDbContext _dbContext;

        public GenreRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Genre?> GetById(Guid genreId)
        {
            return await _dbContext.Genres.FindAsync(genreId);
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        public async Task<Guid> Insert(Genre newGenre)
        {
            _dbContext.Genres.Add(newGenre);
            return newGenre.Id;
        }

        public async Task<bool> Update(Genre updatedGenre)
        {
            _dbContext.Genres.Update(updatedGenre);
            return true;
        }

        public async Task<bool> Delete(Genre genre)
        {
            _dbContext.Genres.Remove(genre);
            return true;
        }
    }
}