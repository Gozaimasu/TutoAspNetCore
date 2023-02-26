namespace RazorPagesMovie.Application.UseCases.EditMovie;

public interface IEditMovieUseCase
{
    Task ExecuteAsync(int id, string title, DateTime releaseDate, string genre, decimal price, string rating, CancellationToken token = default);
    void SetOutputPort(IEditMovieOutputPort outputPort);
}
