using MoviesApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiClient.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetReviewsAsync(string imdbId);
        Task AddReviewAsync(Review review);
    }
}
