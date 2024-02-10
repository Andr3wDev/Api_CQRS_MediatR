using Application.Exceptions;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Users.Command.DeleteUser
{
    public class DeleteUserRequest : IRequest<Guid>
    {
        public DeleteUserRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Guid>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);

            if(user is null)
            {
                throw new NotFoundException($"User with ID {request.Id} not found.");
            }

            var success = await _userRepository.Delete(user);

            return request.Id;
        }
    }
}
