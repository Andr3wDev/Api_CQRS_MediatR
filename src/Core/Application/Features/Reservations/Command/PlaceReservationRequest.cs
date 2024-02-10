using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Request.PlaceReservation
{
    public class PlaceReservationRequest : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }

    public class PlaceReservationRequestHandler : IRequestHandler<PlaceReservationRequest, Guid>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlaceReservationRequestHandler(
            IReservationRepository reservationRepository,
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(PlaceReservationRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetById(request.BookId, cancellationToken);
            if (book is null)
            {
                throw new NotFoundException("Book not found");
            }

            // business logic for placing a reservation
            // TODO

            var success = await _unitOfWork.CommitAsync();

            return Guid.NewGuid();
        }
    }
}
