using Application.Entity;

public interface IGenreRepository
{
    Task<Genre?> GetById(Guid genreId);
    Task<IEnumerable<Genre>> GetAll();
    Task<Guid> Insert(Genre newGenre);
    Task<bool> Update(Genre updatedGenre);
    Task<bool> Delete(Genre genre);
}