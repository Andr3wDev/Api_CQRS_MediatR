using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using API.Controllers;
using Asp.Versioning;
using Application.Features.Users.Query.GetAllUsers;
using Application.Features.Users.Query.GetUserById;
using Application.Features.Users.Command.CreateUser;
using Application.Features.Users.Command.DeleteUser;
using Application.Features.Users.Command.UpdateUser;

namespace API.ApiControllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v1/users")]
    public class UserController : VersionApiController
    {
        [HttpGet]
        [OpenApiOperation("Get all users.", "")]
        public Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return Mediator.Send(new GetAllUsersQuery());
        }

        [HttpGet("{id:guid}")]
        [OpenApiOperation("Get user details.", "")]
        public Task<UserDto> GetByIdAsync(Guid id)
        {
            return Mediator.Send(new GetUserByIdQuery(id));
        }

        [HttpPost]
        [OpenApiOperation("Create a new user.", "")]
        public Task<Guid> CreateAsync(CreateUserRequest request)
        {
            return Mediator.Send(request);
        }

        [HttpDelete("{id:guid}")]
        [OpenApiOperation("Delete a user.", "")]
        public Task<Guid> DeleteAsync(Guid id)
        {
            return Mediator.Send(new DeleteUserRequest(id));
        }

        [HttpPut("{id:guid}")]
        [OpenApiOperation("Update a user.", "")]
        public async Task<ActionResult<Guid>> UpdateAsync(UpdateUserRequest request, Guid id)
        {
            return id != request.Id
                ? BadRequest()
                : Ok(await Mediator.Send(request));
        }
    }
}
