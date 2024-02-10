using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Genres.Command.CreateGenre
{
    public class CreateGenreRequest : IRequest<Guid>
    {
        public string Name { get; set; }
    }

    public class CreateGenreRequestHandler : IRequestHandler<CreateGenreRequest, Guid>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGenreRequestHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateGenreRequest request, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                Name = request.Name
            };

            await _genreRepository.Insert(genre);
            await _unitOfWork.CommitAsync();

            return genre.Id;
        }
    }
}
