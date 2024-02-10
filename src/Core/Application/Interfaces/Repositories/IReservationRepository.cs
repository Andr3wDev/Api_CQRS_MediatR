using Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetBookReservations(Guid bookId);
        Task<IEnumerable<Reservation>> GetUserReservations(Guid userId);
        Task<Reservation?> GetById(Guid reservationId, CancellationToken cancellationToken);
        Task<IEnumerable<Reservation>> GetAll();
        Task<Guid> Insert(Reservation newReservation);
        Task<bool> Update(Reservation updatedReservation);
        Task<bool> Delete(Reservation reservation);
    }
}
