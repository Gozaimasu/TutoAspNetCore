using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieMVC.Models;

public class MovieGenreViewModel
{
    public List<Movie> Movies { get; set; } = default!;
    public SelectList? Genres { get; set; }
    public string? MovieGenre { get; set; }
    public string? SearchString { get; set; }
}
