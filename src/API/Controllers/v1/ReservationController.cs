using Application.Features.Reservations.Query.GetBookReservations;
using Application.Features.Reservations.Query.GetUserReservations;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using API.Controllers;
using Asp.Versioning;
using Application.Features.Reservations.Request.PlaceReservation;
using Application.Features.Reservations.Request.CancelReservation;

namespace API.ApiControllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v1/reservations")]
    public class ReservationController : VersionApiController
    {
        [HttpGet("books/{bookId:guid}")]
        [OpenApiOperation("Get reservations for a specific book.", "")]
        public Task<IEnumerable<ReservationDto>> GetBookReservationsAsync(Guid bookId)
        {
            return Mediator.Send(new GetBookReservationsQuery(bookId));
        }

        [HttpGet("users/{userId:guid}")]
        [OpenApiOperation("Get reservations for a specific user.", "")]
        public Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(Guid userId)
        {
            return Mediator.Send(new GetUserReservationsQuery(userId));
        }

        [HttpPost("place")]
        [OpenApiOperation("Place a reservation for a book.", "")]
        public async Task<ActionResult<Guid>> PlaceAsync(PlaceReservationRequest request)
        {
            var reservationId = await Mediator.Send(request);
            return Ok(reservationId);
        }

        [HttpPost("cancel")]
        [OpenApiOperation("Cancel a reservation for a book.", "")]
        public async Task<ActionResult<Guid>> CancelAsync(CancelReservationRequest request)
        {
            var reservationId = await Mediator.Send(request);
            return Ok(reservationId);
        }
    }
}
