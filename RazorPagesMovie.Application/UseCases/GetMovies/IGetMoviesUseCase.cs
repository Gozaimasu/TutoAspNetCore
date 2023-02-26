namespace RazorPagesMovie.Application.UseCases.GetMovies;

public interface IGetMoviesUseCase
{
    Task ExecuteAsync(string? searchString, string? movieGenre, CancellationToken token = default);
    void SetOutputPort(IGetMoviesOutputPort outputPort);
}
