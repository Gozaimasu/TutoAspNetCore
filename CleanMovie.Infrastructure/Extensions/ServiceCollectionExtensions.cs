using CleanMovie.Application;
using CleanMovie.Infrastructure.DataAccess;
using CleanMovie.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RazorPagesMovieContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RazorPagesMovieContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));

        services.AddScoped<IMovieRepository, MovieRepository>();

        return services;
    }
}
