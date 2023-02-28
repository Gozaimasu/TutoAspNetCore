using CleanMovie.Application.UseCases.CreateMovie;
using CleanMovie.Application.UseCases.GetMovies;
using Microsoft.AspNetCore.Mvc;
using MovieMVC.Models;

namespace MovieMVC.Controllers;

public class MoviesController : Controller, IGetMoviesOutputPort, IOutputPort
{
    private readonly IGetMoviesUseCase _getMoviesUseCase;
    private readonly ICreateMovieUseCase _createMovieUseCase;

    private ActionResult? result;

    public MoviesController(
        IGetMoviesUseCase getMoviesUseCase,
        ICreateMovieUseCase createMovieUseCase)
    {
        _getMoviesUseCase = getMoviesUseCase;
        _createMovieUseCase = createMovieUseCase;
    }

    public async Task<ActionResult> Index(string? searchString, string? movieGenre)
    {
        _getMoviesUseCase.SetOutputPort(this);

        await _getMoviesUseCase.ExecuteAsync(searchString, movieGenre);

        return result!;
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price, Rating")] Movie movie)
    {
        if (!ModelState.IsValid)
        {
            return View(movie);
        }

        _createMovieUseCase.SetOutputPort(this);

        await _createMovieUseCase.ExecuteAsync(movie.Title!, movie.ReleaseDate, movie.Genre!, movie.Price, movie.Rating);
        
        return result!;

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
        result = View(model);
    }
    #endregion

    #region IOutputPort
    void IOutputPort.Ok(CleanMovie.Domain.Movie movie)
    {
        result = RedirectToAction("Index");
    }

    void IOutputPort.NotFound()
    {
        result = NotFound();
    }

    void IOutputPort.Invalid()
    {
        result = View();
    }
    #endregion
}
