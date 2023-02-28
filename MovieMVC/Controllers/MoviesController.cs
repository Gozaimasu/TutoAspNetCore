using CleanMovie.Application.UseCases.GetMovies;
using Microsoft.AspNetCore.Mvc;
using MovieMVC.Models;

namespace MovieMVC.Controllers;

public class MoviesController : Controller, IGetMoviesOutputPort
{
    private readonly IGetMoviesUseCase _getMoviesUseCase;

    public MoviesController(IGetMoviesUseCase getMoviesUseCase)
    {
        _getMoviesUseCase = getMoviesUseCase;
    }

    public async Task<IActionResult> Index(string? searchString, string? movieGenre)
    {
        _getMoviesUseCase.SetOutputPort(this);

        await _getMoviesUseCase.ExecuteAsync(searchString, movieGenre);

        return View();
    }

    #region IGetMoviesOutputPort
    void IGetMoviesOutputPort.Ok(IEnumerable<CleanMovie.Domain.Movie> movie)
    {
        var model = new MovieGenreViewModel()
        {
            Movies = movie.Select(m => new Movie
            {
                Genre = m.Genre,
                Id = m.Id,
                Price = m.Price,
                Rating = m.Rating,
                ReleaseDate = m.ReleaseDate,
                Title = m.Title
            }).ToList()
        };
        ViewData.Model = model;
    }
    #endregion
}
