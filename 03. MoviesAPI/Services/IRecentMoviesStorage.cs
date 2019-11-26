using MoviesApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiClient.Services
{
    public interface IRecentMoviesStorage
    {
        IEnumerable<Movie> GetRecentMovies();
        void AddMovie(Movie movie);
    }
}
