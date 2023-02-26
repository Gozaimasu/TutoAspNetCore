using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Application.UseCases.DeleteMovie;
using RazorPagesMovie.Application.UseCases.GetMovie;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies;

public class DeleteModel : PageModel, IDeleteMovieOutputPort, IGetMovieOutputPort
{
    private readonly IDeleteMovieUseCase _deleteMovieUseCase;
    private readonly IGetMovieUseCase _getMovieUseCase;
    private IActionResult? _viewModel;

    public DeleteModel(IDeleteMovieUseCase deleteMovieUseCase, IGetMovieUseCase getMovieUseCase)
    {
        _deleteMovieUseCase = deleteMovieUseCase;
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        _deleteMovieUseCase.SetOutputPort(this);

        await _deleteMovieUseCase.ExecuteAsync(id.Value);

        return _viewModel!;
    }

    #region IDeleteMovieOutputPort
    void IDeleteMovieOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    void IDeleteMovieOutputPort.Ok()
    {
        _viewModel = RedirectToPage("./Index");
    }
    #endregion

    #region IGetMovieOutputPort

    void IGetMovieOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

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
    #endregion
}