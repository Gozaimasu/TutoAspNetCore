namespace RazorPagesMovie.Application.UseCases.DeleteMovie;

internal class DeleteMovieUseCase : IDeleteMovieUseCase
{
    private readonly IMovieRepository _movieRepository;
    private IDeleteMovieOutputPort? _outputPort;

    public DeleteMovieUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task ExecuteAsync(int id, CancellationToken token = default)
    {
        var movie = await _movieRepository.GetAsync(id, token);

        if(movie == null)
        {
            _outputPort?.NotFound();
            return;
        }

        await _movieRepository.DeleteAsync(movie, token);

        _outputPort?.Ok();
    }

    public void SetOutputPort(IDeleteMovieOutputPort outputPort)
    {
        _outputPort = outputPort;
    }
}
