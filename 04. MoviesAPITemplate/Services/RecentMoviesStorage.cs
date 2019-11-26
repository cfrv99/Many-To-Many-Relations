using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using MoviesApiClient.Models;

namespace MoviesApiClient.Services
{
    public class RecentMoviesStorage : IRecentMoviesStorage
    {
        ConcurrentQueue<Movie> movies = new ConcurrentQueue<Movie>();

        public IEnumerable<Movie> GetRecentMovies()
        {
            return movies.Take(4);
        }

        public void AddMovie(Movie movie)
        {
            if (!movies.Contains(movie))
                movies.Enqueue(movie);

            if (movies.Count > 4)
                movies.TryDequeue(out Movie trash);
        }
    }
}
