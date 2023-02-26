using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Application.UseCases.CreateMovie;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies;

public class CreateModel : PageModel, IOutputPort
{
    private readonly ICreateMovieUseCase _creatingMovieUseCase;
    private IActionResult? _viewModel;

    public CreateModel(ICreateMovieUseCase creatingMovieUseCase)
    {
        _creatingMovieUseCase = creatingMovieUseCase;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Movie Movie { get; set; } = default!;
        

    // To protect from over posting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _creatingMovieUseCase.SetOutputPort(this);

        await _creatingMovieUseCase.ExecuteAsync(Movie.Title!, Movie.ReleaseDate, Movie.Genre!, Movie.Price, Movie.Rating);

        return _viewModel!;
    }

    #region IOutputPort
    void IOutputPort.Ok(Domain.Movie movie)
    {
        _viewModel = RedirectToPage("./Index");
    }

    void IOutputPort.NotFound()
    {
        _viewModel = NotFound();
    }

    void IOutputPort.Invalid()
    {
        _viewModel = Page();
    }
    #endregion
}