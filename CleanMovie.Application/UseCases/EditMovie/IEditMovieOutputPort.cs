using CleanMovie.Domain;

namespace CleanMovie.Application.UseCases.EditMovie;

public interface IEditMovieOutputPort
{
    void Ok(Movie movie);
    void NotFound();
    void Invalid();
}
