using Application.Abstractions;
using Application.Exceptions;
using MediatR;

namespace Application.Features.Genres.Command.DeleteGenre
{
    public class DeleteGenreRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public DeleteGenreRequest(Guid id) => Id = id;
    }

    public class DeleteGenreRequestHandler : IRequestHandler<DeleteGenreRequest, Guid>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGenreRequestHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteGenreRequest request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetById(request.Id);

            if (genre is null)
            {
                throw new NotFoundException("Genre not found");
            }

            await _genreRepository.Delete(genre);
            await _unitOfWork.CommitAsync();

            return request.Id;
        }
    }
}
