namespace Application.Abstractions;

public interface ICustomMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
}