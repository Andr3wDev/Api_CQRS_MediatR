using MediatR;
using Domain.Dto;
using Application.Exceptions;
using Application.Interfaces.Repositories;

namespace Application.Features.Users.Command.UpdateUser
{
    public class UpdateUserRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string NewUsername { get; set; }
        // Add other updated user properties here as needed
    }

    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, Guid>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);
            if (user is null)
            {
                throw new NotFoundException($"User with ID {request.Id} not found.");
            }

            // Update user properties
            user.Username = request.NewUsername;

            // Save changes
            await _userRepository.Update(user);
            return request.Id;
        }
    }
}
