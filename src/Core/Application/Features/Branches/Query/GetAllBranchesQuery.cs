using Application.Abstractions;
using Application.Entity;
using Application.Interfaces.Repositories;
using Domain.Dto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Branches.Query.GetAllBranches
{
	public class GetAllBranchesQuery : IRequest<IEnumerable<BranchDto>>
	{
	}

	public class GetAllBranchesQueryHandler : IRequestHandler<GetAllBranchesQuery, IEnumerable<BranchDto>>
	{
		private readonly IBranchRepository _branchRepository;
		private readonly ICustomMapper _mapper;

		public GetAllBranchesQueryHandler(IBranchRepository branchRepository, ICustomMapper mapper)
		{
			_branchRepository = branchRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BranchDto>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
		{
			var branches = await _branchRepository.GetAll();
			var branchDtos = branches.Select(b => _mapper.Map<Branch, BranchDto>(b));
			return branchDtos;
		}
	}
}
