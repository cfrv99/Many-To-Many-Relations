using Microsoft.Extensions.DependencyInjection;
using MoviesApiClient.Options;
using MoviesApiClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieApi(this IServiceCollection services, Action<MovieApiOptions> options)
        {
            services.AddScoped<IMovieApiService, MovieApiService>();
            services.Configure<MovieApiOptions>(options);
            return services;
        }
    }
}
