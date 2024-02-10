using Application.Abstractions;
using Application.Entity;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Domain.Dto;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Genres.Query.GetGenreById
{
    public class GetGenreByIdQuery : IRequest<GenreDto>
    {
        public Guid GenreId { get; set; }

        public GetGenreByIdQuery(Guid genreId)
        {
            GenreId = genreId;
        }
    }

    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, GenreDto>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ICustomMapper _mapper;

        public GetGenreByIdQueryHandler(IGenreRepository genreRepository, ICustomMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<GenreDto> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetById(request.GenreId);
            if (genre is null)
            {
                throw new NotFoundException("Genre not found");
            }

            return _mapper.Map<Genre, GenreDto>(genre);
        }
    }
}
