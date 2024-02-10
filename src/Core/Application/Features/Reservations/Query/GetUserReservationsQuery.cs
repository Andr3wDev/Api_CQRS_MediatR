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

namespace Application.Features.Reservations.Query.GetUserReservations
{
    public class GetUserReservationsQuery : IRequest<IEnumerable<ReservationDto>>
    {
        public Guid UserId { get; set; }

        public GetUserReservationsQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetUserReservationsQueryHandler : IRequestHandler<GetUserReservationsQuery, IEnumerable<ReservationDto>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomMapper _mapper;

        public GetUserReservationsQueryHandler(IReservationRepository reservationRepository, ICustomMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetUserReservations(request.UserId);
            var reservationDtos = reservations.Select(r => _mapper.Map<Reservation, ReservationDto>(r));
            return reservationDtos;
        }
    }
}
