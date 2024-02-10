using Application.Abstractions;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Branches.Command.DeleteBranch
{
    public class DeleteBranchRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteBranchRequest(Guid id) => Id = id;
    }

    public class DeleteBranchRequestHandler : IRequestHandler<DeleteBranchRequest, Guid>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBranchRequestHandler(IBranchRepository branchRepository, IUnitOfWork unitOfWork)
        {
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetById(request.Id);

            if (branch is null)
            {
                throw new NotFoundException("Branch not found");
            }

            await _branchRepository.Delete(branch);
            await _unitOfWork.CommitAsync();

            return request.Id;
        }
    }
}
