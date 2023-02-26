namespace RazorPagesMovie.Application.UseCases.GetMovie;

public interface IGetMovieUseCase
{
    Task ExecuteAsync(int id, CancellationToken token = default);
    void SetOutputPort(IGetMovieOutputPort outputPort);
}
