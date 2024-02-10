using MediatR;
using Domain.Dto;
using Application.Entity;
using Application.Interfaces.Repositories;

namespace Application.Features.Users.Command.CreateUser
{
    public class CreateUserRequest : IRequest<Guid>
    {
        public string Username { get; set; }
        // Add other user properties here as needed
    }

    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, Guid>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var newUser = new User { Username = request.Username };
            var userId = await _userRepository.Insert(newUser);
            return userId;
        }
    }
}
