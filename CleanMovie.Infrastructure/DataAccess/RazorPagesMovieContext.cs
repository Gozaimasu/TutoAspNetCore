using CleanMovie.Infrastructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanMovie.Infrastructure.DataAccess;

public class RazorPagesMovieContext : DbContext
{
    public RazorPagesMovieContext(DbContextOptions<RazorPagesMovieContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movie { get; set; } = default!;
}