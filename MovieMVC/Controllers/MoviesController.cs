using CleanMovie.Application.UseCases.CreateMovie;
using CleanMovie.Application.UseCases.DeleteMovie;
using CleanMovie.Application.UseCases.EditMovie;
using CleanMovie.Application.UseCases.GetMovie;
using CleanMovie.Application.UseCases.GetMovies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMVC.Models;

namespace MovieMVC.Controllers;

public class MoviesController : Controller, 
                                IGetMoviesOutputPort, 
                                IOutputPort, 
                                IGetMovieOutputPort, 
                                IEditMovieOutputPort,
                                IDeleteMovieOutputPort
{
    private readonly IGetMoviesUseCase _getMoviesUseCase;
    private readonly ICreateMovieUseCase _createMovieUseCase;
    private readonly IEditMovieUseCase _editMovieUseCase;
    private readonly IGetMovieUseCase _getMovieUseCase;
    private readonly IDeleteMovieUseCase _deleteMovieUseCase;
    private ActionResult? _result;

    public MoviesController(
        IGetMoviesUseCase getMoviesUseCase,
        ICreateMovieUseCase createMovieUseCase,
        IEditMovieUseCase editMovieUseCase,
        IGetMovieUseCase getMovieUseCase,
        IDeleteMovieUseCase deleteMovieUseCase)
    {
        _getMoviesUseCase = getMoviesUseCase;
        _createMovieUseCase = createMovieUseCase;
        _editMovieUseCase = editMovieUseCase;
        _getMovieUseCase = getMovieUseCase;
        _deleteMovieUseCase = deleteMovieUseCase;
    }

    #region Index
    public async Task<ActionResult> Index(string? searchString, string? movieGenre)
    {
        _getMoviesUseCase.SetOutputPort(this);

        await _getMoviesUseCase.ExecuteAsync(searchString, movieGenre);

        return _result!;
    }
    #endregion

    #region Create
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

        return _result!;

    }
    #endregion

    #region Edit
    public async Task<ActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        _getMovieUseCase.SetOutputPort(this);
        await _getMovieUseCase.ExecuteAsync(id.Value);

        return _result!;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        _editMovieUseCase.SetOutputPort(this);
        await _editMovieUseCase.ExecuteAsync(id, movie.Title!, movie.ReleaseDate, movie.Genre!, movie.Price, movie.Rating);

        return _result!;
    }
    #endregion

    #region Details
    public async Task<ActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        _getMovieUseCase.SetOutputPort(this);
        await _getMovieUseCase.ExecuteAsync(id.Value);

        return _result!;
    }
    #endregion

    #region Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        _getMovieUseCase.SetOutputPort(this);
        await _getMovieUseCase.ExecuteAsync(id.Value);

        return _result!;
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _deleteMovieUseCase.SetOutputPort(this);
        await _deleteMovieUseCase.ExecuteAsync(id);

        return _result!;
    }
    #endregion

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
        _result = View(model);
    }
    #endregion

    #region IOutputPort
    void IOutputPort.Ok(CleanMovie.Domain.Movie movie)
    {
        _result = RedirectToAction("Index");
    }

    void IOutputPort.NotFound()
    {
        _result = NotFound();
    }

    void IOutputPort.Invalid()
    {
        _result = View();
    }
    #endregion

    #region IGetMovieOutputPort
    void IGetMovieOutputPort.Ok(CleanMovie.Domain.Movie movie)
    {
        var model = new Movie
        {
            Genre = movie.Genre,
            Id = movie.Id,
            Price = movie.Price,
            Rating = movie.Rating,
            ReleaseDate = movie.ReleaseDate,
            Title = movie.Title
        };
        _result = View(model);
    }

    void IGetMovieOutputPort.NotFound()
    {
        _result = NotFound();
    }
    #endregion

    #region IEditMovieOutputPort
    void IEditMovieOutputPort.Ok(CleanMovie.Domain.Movie movie)
    {
        _result = RedirectToAction("Index");
    }

    void IEditMovieOutputPort.NotFound()
    {
        _result = NotFound();
    }

    void IEditMovieOutputPort.Invalid()
    {
        _result = View();
    }
    #endregion

    #region IDeleteMovieOutputPort
    void IDeleteMovieOutputPort.Ok()
    {
        _result = RedirectToAction("Index");
    }

    void IDeleteMovieOutputPort.NotFound()
    {
        _result= NotFound();
    }
    #endregion
}
