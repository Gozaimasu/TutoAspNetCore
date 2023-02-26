using RazorPagesMovie.Domain;

namespace RazorPagesMovie.Application.UseCases.GetMovies;

public interface IGetMoviesOutputPort
{
    void Ok(IEnumerable<Movie> movie);
}
