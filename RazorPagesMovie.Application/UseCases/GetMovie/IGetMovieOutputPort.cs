using CleanMovie.Domain;

namespace RazorPagesMovie.Application.UseCases.GetMovie;

public interface IGetMovieOutputPort
{
    void Ok(Movie movie);
    void NotFound();
}
