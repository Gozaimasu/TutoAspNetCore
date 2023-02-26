using CleanMovie.Domain;

namespace CleanMovie.Application.UseCases.DeleteMovie;

public interface IDeleteMovieOutputPort
{
    void Ok();
    void NotFound();
}
