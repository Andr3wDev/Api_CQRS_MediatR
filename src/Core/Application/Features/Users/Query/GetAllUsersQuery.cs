using MediatR;
using Domain.Dto;
using Application.Interfaces.Repositories;
using Application.Abstractions;
using Application.Entity;

namespace Application.Features.Users.Query.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, ICustomMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map< IEnumerable<User>,IEnumerable <UserDto>>(users);
        }
    }
}
