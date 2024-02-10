using Application.Abstractions;
using Application.Entity;
using Application.Interfaces.Repositories;
using Domain.Dto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Genres.Query.GetAllGenres
{
    public class GetAllGenresQuery : IRequest<IEnumerable<GenreDto>>
    {
    }

    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreDto>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ICustomMapper _mapper;

        public GetAllGenresQueryHandler(IGenreRepository genreRepository, ICustomMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _genreRepository.GetAll();
            var genreDtos = genres.Select(g => _mapper.Map<Genre, GenreDto>(g));
            return genreDtos;
        }
    }
}
