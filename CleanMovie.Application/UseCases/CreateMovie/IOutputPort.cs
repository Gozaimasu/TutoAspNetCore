using CleanMovie.Domain;

namespace CleanMovie.Application.UseCases.CreateMovie;

public interface IOutputPort
{
    void Ok(Movie movie);
    void NotFound();
    void Invalid();
}
