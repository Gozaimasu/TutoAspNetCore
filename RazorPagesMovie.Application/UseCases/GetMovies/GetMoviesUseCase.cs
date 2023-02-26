namespace RazorPagesMovie.Application.UseCases.GetMovies;

internal class GetMoviesUseCase : IGetMoviesUseCase
{
    private readonly IMovieRepository _movieRepository;
    private IGetMoviesOutputPort? _outputPort;

    public GetMoviesUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task ExecuteAsync(string? searchString, string? movieGenre, CancellationToken token = default)
    {
        var movies = await _movieRepository.ListAsync(token);

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(s => s.Title != null && s.Title.Contains(searchString));
        }

        if (!string.IsNullOrEmpty(movieGenre))
        {
            movies = movies.Where(x => x.Genre == movieGenre);
        }

        _outputPort?.Ok(movies);
    }

    public void SetOutputPort(IGetMoviesOutputPort outputPort)
    {
        _outputPort = outputPort;
    }
}
