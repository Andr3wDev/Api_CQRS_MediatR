using Application.Features.Authors.Command.UpdateAuthor;
using Application.Features.Authors.Command.CreateAuthor;
using Application.Features.Authors.Command.DeleteAuthor;
using Application.Features.Authors.Query.GetAllAuthors;
using Application.Features.Authors.Query.GetAuthorById;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using NSwag.Annotations;
using API.Controllers;

namespace API.ApiControllers.v1;

[ApiVersion("1.0")]
[Route("api/v1/authors")]
public class AuthorsController : VersionApiController
{
    [HttpGet]
    [OpenApiOperation("Get all authors.", "")]
    public Task<IEnumerable<AuthorDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllAuthorsQuery());
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get author details.", "")]
    public Task<AuthorDto> GetByIdAsync(Guid id)
    {
        return Mediator.Send(new GetAuthorByIdQuery(id));
    }

    [HttpPost]
    [OpenApiOperation("Create an new author.", "")]
    public Task<Guid> CreateAsync(CreateAuthorRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete an author.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAuthorRequest(id));
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update an author.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAuthorRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }
}