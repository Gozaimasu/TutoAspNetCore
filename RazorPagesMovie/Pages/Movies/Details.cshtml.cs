using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Application.UseCases.GetMovie;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies;

public class DetailsModel : PageModel, IGetMovieOutputPort
{
    private readonly IGetMovieUseCase _getMovieUseCase;
    private IActionResult? _viewModel;

    public DetailsModel(IGetMovieUseCase getMovieUseCase)
    {
        _getMovieUseCase = getMovieUseCase;
    }

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

    #region IGetMovieOutputPort
    void IGetMovieOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    void IGetMovieOutputPort.Ok(Domain.Movie movie)
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