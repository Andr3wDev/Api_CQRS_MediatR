using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using Domain.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Query.GetBookReservations
{
    public class GetBookReservationsQuery : IRequest<IEnumerable<ReservationDto>>
    {
        public Guid BookId { get; set; }

        public GetBookReservationsQuery(Guid bookId)
        {
            BookId = bookId;
        }
    }

    public class GetBookReservationsQueryHandler : IRequestHandler<GetBookReservationsQuery, IEnumerable<ReservationDto>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomMapper _mapper;

        public GetBookReservationsQueryHandler(IReservationRepository reservationRepository, ICustomMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(GetBookReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetBookReservations(request.BookId);
            var reservationDtos = reservations.Select(r => _mapper.Map<Reservation, ReservationDto>(r));
            return reservationDtos;
        }
    }
}
