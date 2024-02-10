using Application.Features.Branches.Command.CreateBranch;
using Application.Features.Branches.Command.DeleteBranch;
using Application.Features.Branches.Command.UpdateBranch;
using Application.Features.Branches.Query.GetAllBranches;
using Application.Features.Branches.Query.GetBranchById;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using API.Controllers;
using Asp.Versioning;

namespace API.ApiControllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v1/branches")]
    public class BranchController : VersionApiController
    {
        [HttpGet]
        [OpenApiOperation("Get all library branches.", "")]
        public Task<IEnumerable<BranchDto>> GetAllAsync()
        {
            return Mediator.Send(new GetAllBranchesQuery());
        }

        [HttpGet("{id:guid}")]
        [OpenApiOperation("Get library branch details.", "")]
        public Task<BranchDto> GetByIdAsync(Guid id)
        {
            return Mediator.Send(new GetBranchByIdQuery(id));
        }

        [HttpPost]
        [OpenApiOperation("Create a new library branch.", "")]
        public Task<Guid> CreateAsync(CreateBranchRequest request)
        {
            return Mediator.Send(request);
        }

        [HttpDelete("{id:guid}")]
        [OpenApiOperation("Delete a library branch.", "")]
        public Task<Guid> DeleteAsync(Guid id)
        {
            return Mediator.Send(new DeleteBranchRequest(id));
        }

        [HttpPut("{id:guid}")]
        [OpenApiOperation("Update a library branch.", "")]
        public async Task<ActionResult<Guid>> UpdateAsync(UpdateBranchRequest request, Guid id)
        {
            return id != request.Id
                ? BadRequest()
                : Ok(await Mediator.Send(request));
        }
    }
}
