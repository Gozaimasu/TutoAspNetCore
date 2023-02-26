using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Application.UseCases.EditMovie;
using RazorPagesMovie.Application.UseCases.GetMovie;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies;

public class EditModel : PageModel, IEditMovieOutputPort, IGetMovieOutputPort
{
    private readonly IEditMovieUseCase _editMovieUseCase;
    private readonly IGetMovieUseCase _getMovieUseCase;
    private IActionResult? _viewModel;

    public EditModel(IEditMovieUseCase editMovieUseCase, IGetMovieUseCase getMovieUseCase)
    {
        _editMovieUseCase = editMovieUseCase;
        _getMovieUseCase = getMovieUseCase;
    }

    [BindProperty]
    public Movie Movie { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        _getMovieUseCase.SetOutputPort(this);
        await _getMovieUseCase.ExecuteAsync(id.Value);

        return _viewModel!;
    }

    // To protect from over posting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _editMovieUseCase.SetOutputPort(this);
        await _editMovieUseCase.ExecuteAsync(Movie.Id, Movie.Title!, Movie.ReleaseDate, Movie.Genre!, Movie.Price, Movie.Rating);

        return _viewModel!;
    }

    #region IEditMovieOutputPort
    void IEditMovieOutputPort.Invalid()
    {
        throw new NotImplementedException();
    }

    void IEditMovieOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    void IEditMovieOutputPort.Ok(CleanMovie.Domain.Movie movie)
    {
        Movie = new Movie
        {
            Genre = movie.Genre,
            Id = movie.Id,
            Price = movie.Price,
            Rating = movie.Rating,
            ReleaseDate = movie.ReleaseDate,
            Title = movie.Title
        };
        _viewModel = RedirectToPage("./Index");
    }
    #endregion

    #region IGetMovieOutputPort
    void IGetMovieOutputPort.Ok(CleanMovie.Domain.Movie movie)
    {
        Movie = new Movie
        {
            Genre = movie.Genre,
            Id = movie.Id,
            Price = movie.Price,
            Rating = movie.Rating,
            ReleaseDate = movie.ReleaseDate,
            Title = movie.Title
        };
        _viewModel = Page();
    }

    void IGetMovieOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }
    #endregion
}