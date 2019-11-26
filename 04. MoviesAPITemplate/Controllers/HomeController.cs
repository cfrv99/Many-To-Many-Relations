using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApiClient.Models;
using MoviesApiClient.Services;
using MoviesApiClient.ViewModels;

namespace MoviesApiClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieApiService movieApiService;
        private readonly IRecentMoviesStorage recentMoviesStorage;
        private readonly IReviewService reviewService;

        public HomeController(IMovieApiService movieApiService, IRecentMoviesStorage recentMoviesStorage, IReviewService reviewService)
        {
            this.movieApiService = movieApiService;
            this.recentMoviesStorage = recentMoviesStorage;
            this.reviewService = reviewService;
        }

        public IActionResult Index()
        {
            var result = recentMoviesStorage.GetRecentMovies();
            return View(result);
        }

        // Home/Search?title=Matrix
        public async Task<IActionResult> Search(string title, int page = 1)
        {
            var result = await movieApiService.SearchByTitleAsync(title, page);
            
            var model = new SearchViewModel
            {
                Movies = result.Movies,
                Title = title,
                TotalResults = result.TotalResults,
                TotalPages = (int)Math.Ceiling(result.TotalResults / 10.0),
                CurrentPage = page
            };

            return View(model);
        }

        public async Task<IActionResult> SearchResult(string title, int page = 1)
        {
            var result = await movieApiService.SearchByTitleAsync(title, page);
            return PartialView("_MovieListPartial", result.Movies);
        }

        // Home/Movie/123123
        public async Task<IActionResult> Movie(string id)
        {
            var movie = await movieApiService.SearchByIdAsync(id);
            var reviews = await reviewService.GetReviewsAsync(id);
            recentMoviesStorage.AddMovie(movie);

            var model = new MovieDetailsViewModel
            {
                Movie = movie,
                Reviews = reviews
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PostReview(Review review, string name)
        {
            await reviewService.AddReviewAsync(review);
            return RedirectToAction("Movie", new { id = review.ImdbId });
        }

        public async Task<IActionResult> MovieModal(string id)
        {
            var result = await movieApiService.SearchByIdAsync(id);
            recentMoviesStorage.AddMovie(result);
            return PartialView("_MovieModalPartial", result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
