using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMVC.Models;

public class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; }

    [Display(Name = "Release Date")]
    public DateTime ReleaseDate { get; set; }

    public string? Genre { get; set; }

    public decimal Price { get; set; }

    public string Rating { get; set; } = string.Empty;
}
