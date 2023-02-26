using CleanMovie.Application;
using CleanMovie.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RazorPagesMovie.Infrastructure.DataAccess.Repositories;

internal class MovieRepository : IMovieRepository
{
    private readonly RazorPagesMovieContext _dbContext;

    public MovieRepository(RazorPagesMovieContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Movie> AddAsync(Movie movie, CancellationToken token = default)
    {
        Models.Movie movie1 = new()
        {
            Title = movie.Title,
            ReleaseDate = movie.ReleaseDate,
            Genre = movie.Genre,
            Price = movie.Price,
            Rating = movie.Rating
        };

        EntityEntry<Models.Movie> entityEntry = await _dbContext.Movie.AddAsync(movie1, token);
        await _dbContext.SaveChangesAsync(token);

        return new Movie()
        {
            Genre = movie.Genre,
            Id = entityEntry.Entity.Id,
            Price = movie.Price,
            Rating = movie.Rating,
            ReleaseDate = movie.ReleaseDate,
            Title = movie.Title
        };
    }

    public async Task DeleteAsync(Movie movie, CancellationToken token = default)
    {
        await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Movie WHERE Id=@p0", new object[] { movie.Id }, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<Movie?> GetAsync(int id, CancellationToken token = default)
    {
        var movie = await _dbContext.Movie.FirstOrDefaultAsync(m => m.Id == id, cancellationToken: token);

        if (movie == null)
        {
            return null;
        }

        return new Movie()
        {
            Genre = movie.Genre,
            Id = movie.Id,
            Price = movie.Price,
            Rating = movie.Rating,
            ReleaseDate = movie.ReleaseDate,
            Title = movie.Title
        };
    }

    public Task<IEnumerable<Movie>> ListAsync(CancellationToken token = default)
    {
        IEnumerable<Movie> movies = _dbContext.Movie.Select(m => new Movie
        {
            Genre = m.Genre,
            Id = m.Id,
            Price = m.Price,
            Rating = m.Rating,
            ReleaseDate = m.ReleaseDate,
            Title = m.Title
        });

        return Task.FromResult(movies);
    }

    public async Task UpdateAsync(Movie movie, CancellationToken token = default)
    {
        var movie1 = await _dbContext.Movie.FirstOrDefaultAsync(m => m.Id == movie.Id, cancellationToken: token);

        if (movie1 == null)
        {
            return;
        }

        movie1.Genre = movie.Genre;
        movie1.Price = movie.Price;
        movie1.Rating = movie.Rating;
        movie1.ReleaseDate = movie.ReleaseDate;
        movie1.Title = movie.Title;

        await _dbContext.SaveChangesAsync(token);
    }
}
