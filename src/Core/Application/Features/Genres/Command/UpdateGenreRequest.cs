using Application.Abstractions;
using Application.Interfaces.Repositories;
using Application.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Application.Features.Genres.Command.UpdateGenre
{
    public class UpdateGenreRequest : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateGenreRequestHandler : IRequestHandler<UpdateGenreRequest>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGenreRequestHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateGenreRequest request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetById(request.Id);

            if (genre is null)
            {
                throw new NotFoundException("Genre not found");
            }

            genre.Name = request.Name;

            await _genreRepository.Update(genre);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
