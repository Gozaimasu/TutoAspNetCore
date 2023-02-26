using RazorPagesMovie.Domain;

namespace RazorPagesMovie.Application.UseCases.CreateMovie;

public interface IOutputPort
{
    void Ok(Movie movie);
    void NotFound();
    void Invalid();
}
