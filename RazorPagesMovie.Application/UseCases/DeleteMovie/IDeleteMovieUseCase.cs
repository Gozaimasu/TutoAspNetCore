namespace RazorPagesMovie.Application.UseCases.DeleteMovie;

public interface IDeleteMovieUseCase
{
    Task ExecuteAsync(int id, CancellationToken token = default);
    void SetOutputPort(IDeleteMovieOutputPort outputPort);
}
