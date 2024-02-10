using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Request.CancelReservation
{
    public class CancelReservationRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public CancelReservationRequest(Guid id) => Id = id;
    }

    public class CancelReservationRequestHandler : IRequestHandler<CancelReservationRequest, Guid>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelReservationRequestHandler(
            IReservationRepository reservationRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CancelReservationRequest request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetById(request.Id, cancellationToken);
            if (reservation is null)
            {
                throw new NotFoundException("Reservation not found");
            }

            // business logic for canceling
            // TODO 

            // Commit changes to the database
            var success = await _unitOfWork.CommitAsync();

            return Guid.NewGuid();
        }
    }
}