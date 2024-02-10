using Application.Abstractions;
using Application.Entity;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Domain.Dto;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Branches.Query.GetBranchById
{
    public class GetBranchByIdQuery : IRequest<BranchDto>
    {
        public Guid BranchId { get; set; }

        public GetBranchByIdQuery(Guid branchId)
        {
            BranchId = branchId;
        }
    }

    public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, BranchDto>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly ICustomMapper _mapper;

        public GetBranchByIdQueryHandler(IBranchRepository branchRepository, ICustomMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<BranchDto> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetById(request.BranchId);
            if (branch is null)
            {
                throw new NotFoundException("Branch not found");
            }

            return _mapper.Map<Branch, BranchDto>(branch);
        }
    }
}
