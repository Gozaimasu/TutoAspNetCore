using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Infrastructure.DataAccess.Models;

namespace RazorPagesMovie.Infrastructure.DataAccess;

public class RazorPagesMovieContext : DbContext
{
    public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movie { get; set; } = default!;
}