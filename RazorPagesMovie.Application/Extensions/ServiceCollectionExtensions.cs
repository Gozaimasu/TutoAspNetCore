using RazorPagesMovie.Application.UseCases.CreateMovie;
using RazorPagesMovie.Application.UseCases.DeleteMovie;
using RazorPagesMovie.Application.UseCases.EditMovie;
using RazorPagesMovie.Application.UseCases.GetMovie;
using RazorPagesMovie.Application.UseCases.GetMovies;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateMovieUseCase, CreateMovieUseCase>();
        services.AddScoped<IDeleteMovieUseCase, DeleteMovieUseCase>();
        services.AddScoped<IEditMovieUseCase, EditMovieUseCase>();
        services.AddScoped<IGetMovieUseCase, GetMovieUseCase>();
        services.AddScoped<IGetMoviesUseCase, GetMoviesUseCase>();

        return services;
    }
}
