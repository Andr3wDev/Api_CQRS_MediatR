using Application.Entity;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book?> GetById(Guid bookId, CancellationToken cancellationToken)
        {
            return await _dbContext.Books.FindAsync(bookId);
        }

        public async Task<IEnumerable<Book>> GetByAuthor(Guid authorId, CancellationToken cancellationToken)
        {
            return await _dbContext.Books
                .Where(book => book.AuthorId == authorId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Guid> Insert(Book newBook)
        {
            _dbContext.Books.Add(newBook);
            return newBook.Id;
        }

        public async Task<bool> Update(Book updatedBook, CancellationToken cancellationToken)
        {
            _dbContext.Books.Update(updatedBook);
            return true;
        }

        public async Task<bool> Delete(Book book)
        {
            _dbContext.Books.Remove(book);
            return true;
        }

        public async Task<bool> CheckTitleExistsByAuthor(string title, Guid authorId, CancellationToken cancellationToken)
        {
            var exists = await _dbContext.Books.AnyAsync(b => b.Title == title && b.AuthorId == authorId);
            return exists;
        }

        public async Task<IEnumerable<Book>> SearchBooks(string title)
        {
            return await _dbContext.Books
                .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
    }
}
