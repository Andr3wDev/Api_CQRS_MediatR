using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Branches.Command.CreateBranch
{
    public class CreateBranchRequest : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class CreateBranchRequestHandler : IRequestHandler<CreateBranchRequest, Guid>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBranchRequestHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork)
        {
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
        {
            var branch = new Branch
            {
                Name = request.Name,
                Location = request.Location
            };

            await _branchRepository.Insert(branch);
            await _unitOfWork.CommitAsync();

            return branch.Id;
        }
    }
}
