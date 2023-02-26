namespace RazorPagesMovie.Application.UseCases.CreateMovie;

public interface ICreateMovieUseCase
{
    Task ExecuteAsync(string title, DateTime releaseDate, string genre, decimal price, string rating, CancellationToken token = default);
    void SetOutputPort(IOutputPort outputPort);
}
