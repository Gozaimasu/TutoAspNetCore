using CleanMovie.Application;
using CleanMovie.Domain;

namespace CleanMovie.Application.UseCases.CreateMovie;

internal class CreateMovieUseCase : ICreateMovieUseCase
{
    private readonly IMovieRepository _movieRepository;
    private IOutputPort? _outputPort;

    public CreateMovieUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task ExecuteAsync(string title, DateTime releaseDate, string genre, decimal price, string rating, CancellationToken token = default)
    {
        Movie movie = new()
        {
            Title = title,
            ReleaseDate = releaseDate,
            Genre = genre,
            Price = price,
            Rating = rating
        };

        movie = await _movieRepository.AddAsync(movie, token);

        _outputPort?.Ok(movie);
    }

    public void SetOutputPort(IOutputPort outputPort)
    {
        _outputPort = outputPort;
    }
}
