using CleanMovie.Application;
using CleanMovie.Domain;

namespace CleanMovie.Application.UseCases.GetMovie;

internal class GetMovieUseCase : IGetMovieUseCase
{
    private readonly IMovieRepository _movieRepository;
    private IGetMovieOutputPort? _outputPort;

    public GetMovieUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task ExecuteAsync(int id, CancellationToken token = default)
    {
        Movie? movie = await _movieRepository.GetAsync(id, token);

        if (movie == null)
        {
            _outputPort?.NotFound();
            return;
        }

        _outputPort?.Ok(movie);
    }

    public void SetOutputPort(IGetMovieOutputPort outputPort)
    {
        _outputPort = outputPort;
    }
}
