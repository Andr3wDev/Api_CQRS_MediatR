using Application.Features.Loans.Query.GetBookLoans;
using Application.Features.Loans.Query.GetUserLoans;
using Application.Features.Loans.Request.BorrowBook;
using Application.Features.Loans.Request.ReturnBook;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using API.Controllers;
using Asp.Versioning;

namespace API.ApiControllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v1/loans")]
    public class LoanController : VersionApiController
    {
        [HttpGet("books/{bookId:guid}")]
        [OpenApiOperation("Get loans for a specific book.", "")]
        public Task<IEnumerable<LoanDto>> GetBookLoansAsync(Guid bookId)
        {
            return Mediator.Send(new GetBookLoansQuery(bookId));
        }

        [HttpGet("users/{userId:guid}")]
        [OpenApiOperation("Get loans for a specific user.", "")]
        public Task<IEnumerable<LoanDto>> GetUserLoansAsync(Guid userId)
        {
            return Mediator.Send(new GetUserLoansQuery(userId));
        }

        [HttpPost("borrow")]
        [OpenApiOperation("Borrow a book.", "")]
        public async Task<ActionResult<Guid>> BorrowAsync(BorrowBookRequest request)
        {
            var loanId = await Mediator.Send(request);
            return Ok(loanId);
        }

        [HttpPost("return")]
        [OpenApiOperation("Return a book.", "")]
        public async Task<ActionResult<Guid>> ReturnAsync(ReturnBookRequest request)
        {
            var loanId = await Mediator.Send(request);
            return Ok(loanId);
        }
    }
}
