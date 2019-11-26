using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesApiClient.Models;

namespace MoviesApiClient.Services
{
    public class ReviewService : IReviewService
    {
        private readonly MovieAppDbContext context;

        public ReviewService(MovieAppDbContext context)
        {
            this.context = context;
        }

        public async Task AddReviewAsync(Review review)
        {
            context.Reviews.Add(review);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsAsync(string imdbId)
        {
            return await context.Reviews.Where(x => x.ImdbId == imdbId).ToListAsync();
        }
    }
}
