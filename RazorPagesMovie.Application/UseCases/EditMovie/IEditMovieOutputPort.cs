using CleanMovie.Domain;

namespace RazorPagesMovie.Application.UseCases.EditMovie;

public interface IEditMovieOutputPort
{
    void Ok(Movie movie);
    void NotFound();
    void Invalid();
}
