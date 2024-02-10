using Application.Abstractions;
using MapsterMapper;

namespace Infrastructure.Mapping;

public class CustomMapper : ICustomMapper
{
    private readonly IMapper _mapper;

    public CustomMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TSource, TDestination>(TSource source)
        => _mapper.Map<TSource, TDestination>(source);
}