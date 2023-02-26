using CleanMovie.Application;

namespace CleanMovie.Application.UseCases.EditMovie;

internal class EditMovieUseCase : IEditMovieUseCase
{
    private readonly IMovieRepository _movieRepository;
    private IEditMovieOutputPort? _outputPort;

    public EditMovieUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task ExecuteAsync(int id, string title, DateTime releaseDate, string genre, decimal price, string rating, CancellationToken token = default)
    {
        var movie = await _movieRepository.GetAsync(id, token);

        if (movie == null)
        {
            _outputPort?.NotFound();
            return;
        }

        movie.Title = title;
        movie.ReleaseDate = releaseDate;
        movie.Genre = genre;
        movie.Price = price;
        movie.Rating = rating;

        await _movieRepository.UpdateAsync(movie, token);

        _outputPort?.Ok(movie);
    }

    public void SetOutputPort(IEditMovieOutputPort outputPort)
    {
        _outputPort = outputPort;
    }
}
