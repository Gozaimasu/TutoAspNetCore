using CleanMovie.Domain;

namespace RazorPagesMovie.Application;

public interface IMovieRepository
{
    Task<Movie> AddAsync(Movie movie, CancellationToken token = default);
    Task DeleteAsync(Movie movie, CancellationToken token = default);
    Task<Movie?> GetAsync(int id, CancellationToken token = default);
    Task<IEnumerable<Movie>> ListAsync(CancellationToken token = default);
    Task UpdateAsync(Movie movie, CancellationToken token = default);
}
