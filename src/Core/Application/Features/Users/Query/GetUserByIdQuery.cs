using MediatR;
using Domain.Dto;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Abstractions;
using Application.Entity;

namespace Application.Features.Users.Query.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public GetUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, ICustomMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId);
            if (user is null)
            {
                throw new NotFoundException($"User with ID {request.UserId} not found.");
            }

            return _mapper.Map<User,UserDto>(user);
        }
    }
}
