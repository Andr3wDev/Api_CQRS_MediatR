using API.Controllers;
using Application.Features.Books.Command.CreateBook;
using Application.Features.Books.Command.DeleteBook;
using Application.Features.Books.Command.UpdateBook;
using Application.Features.Books.Query.GetAllBooks;
using Application.Features.Books.Query.GetBookById;
using Asp.Versioning;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace API.ApiControllers.v1;

[ApiVersion("1.0")]
[Route("api/v1/books")]
public class BooksController : VersionApiController
{
    [HttpGet]
    [OpenApiOperation("Get all books.", "")]
    public Task<IEnumerable<BookDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllBooksQuery());
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get book details.", "")]
    public Task<BookDto> GetByIdAsync(Guid id)
    {
        return Mediator.Send(new GetBookByIdQuery(id));
    }

    [HttpPost]
    [OpenApiOperation("Create an new book.", "")] 
    public Task<Guid> CreateAsync(CreateBookRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a book.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBookRequest(id));
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update an book.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBookRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }
}