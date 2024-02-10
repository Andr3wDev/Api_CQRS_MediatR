using Application.Features.Genres.Command.CreateGenre;
using Application.Features.Genres.Command.DeleteGenre;
using Application.Features.Genres.Command.UpdateGenre;
using Application.Features.Genres.Query.GetAllGenres;
using Application.Features.Genres.Query.GetGenreById;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using API.Controllers;
using Asp.Versioning;

namespace API.ApiControllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v1/genres")]
    public class GenreController : VersionApiController
    {
        [HttpGet]
        [OpenApiOperation("Get all genres.", "")]
        public Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            return Mediator.Send(new GetAllGenresQuery());
        }

        [HttpGet("{id:guid}")]
        [OpenApiOperation("Get genre details.", "")]
        public Task<GenreDto> GetByIdAsync(Guid id)
        {
            return Mediator.Send(new GetGenreByIdQuery(id));
        }

        [HttpPost]
        [OpenApiOperation("Create a new genre.", "")]
        public Task<Guid> CreateAsync(CreateGenreRequest request)
        {
            return Mediator.Send(request);
        }

        [HttpDelete("{id:guid}")]
        [OpenApiOperation("Delete a genre.", "")]
        public Task<Guid> DeleteAsync(Guid id)
        {
            return Mediator.Send(new DeleteGenreRequest(id));
        }

        [HttpPut("{id:guid}")]
        [OpenApiOperation("Update a genre.", "")]
        public async Task<ActionResult<Guid>> UpdateAsync(UpdateGenreRequest request, Guid id)
        {
            return id != request.Id
                ? BadRequest()
                : Ok(await Mediator.Send(request));
        }
    }
}
