using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Application.UseCases.GetMovies;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies;

public class IndexModel : PageModel, IGetMoviesOutputPort
{
    private readonly IGetMoviesUseCase _getMoviesUseCase;

    public IndexModel(IGetMoviesUseCase getMoviesUseCase)
    {
        this._getMoviesUseCase = getMoviesUseCase;
    }

    public IList<Movie> Movie { get;set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public SelectList? Genres { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? MovieGenre { get; set; }

    #region IGetMoviesOutputPort
    void IGetMoviesOutputPort.Ok(IEnumerable<CleanMovie.Domain.Movie> movie)
    {
        Movie = movie.Select(m => new Movie
        {
            Genre = m.Genre,
            Id = m.Id,
            Price = m.Price,
            Rating = m.Rating,
            ReleaseDate = m.ReleaseDate,
            Title = m.Title
        }).ToList();
    }
    #endregion

    public async Task OnGetAsync()
    {
        _getMoviesUseCase.SetOutputPort(this);

        await _getMoviesUseCase.ExecuteAsync(SearchString, MovieGenre);
        //// Use LINQ to get list of genres.
        //IQueryable<string> genreQuery = from m in _context.Movie
        //                                orderby m.Genre
        //                                select m.Genre;
        //
        //Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
    }
}