using CleanMovie.Domain;

namespace CleanMovie.Application.UseCases.GetMovie;

public interface IGetMovieOutputPort
{
    void Ok(Movie movie);
    void NotFound();
}
