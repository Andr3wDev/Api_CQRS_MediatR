using Application.Entity;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryDbContext _dbContext;

        public ReservationRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Reservation>> GetBookReservations(Guid bookId)
        {
            return await _dbContext.Reservations
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetUserReservations(Guid userId)
        {
            return await _dbContext.Reservations
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Reservation?> GetById(Guid reservationId, CancellationToken cancellationToken)
        {
            return await _dbContext.Reservations.FindAsync(reservationId);
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<Guid> Insert(Reservation newReservation)
        {
            _dbContext.Reservations.Add(newReservation);
            return newReservation.Id;
        }

        public async Task<bool> Update(Reservation updatedReservation)
        {
            _dbContext.Reservations.Update(updatedReservation);
            return true;
        }

        public async Task<bool> Delete(Reservation reservation)
        {
            _dbContext.Reservations.Remove(reservation);
            return true;
        }
    }
}
