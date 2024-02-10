using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Application.Features.Branches.Command.UpdateBranch
{
    public class UpdateBranchRequest : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class UpdateBranchRequestHandler : IRequestHandler<UpdateBranchRequest>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBranchRequestHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork)
        {
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateBranchRequest request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetById(request.Id);

            if (branch is null)
            {
                throw new NotFoundException("Branch not found");
            }

            branch.Name = request.Name;
            branch.Location = request.Location;

            await _branchRepository.Update(branch);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
