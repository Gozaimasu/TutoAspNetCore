using CleanMovie.Application.UseCases.CreateMovie;
using CleanMovie.Application.UseCases.DeleteMovie;
using CleanMovie.Application.UseCases.EditMovie;
using CleanMovie.Application.UseCases.GetMovie;
using CleanMovie.Application.UseCases.GetMovies;

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
