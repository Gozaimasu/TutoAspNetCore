using CleanMovie.Domain;

namespace CleanMovie.Application.UseCases.GetMovies;

public interface IGetMoviesOutputPort
{
    void Ok(IEnumerable<Movie> movie);
}
