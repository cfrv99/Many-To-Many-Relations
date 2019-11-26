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

        public HomeController(IMovieApiService movieApiService, IRecentMoviesStorage recentMoviesStorage)
        {
            this.movieApiService = movieApiService;
            this.recentMoviesStorage = recentMoviesStorage;
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
            var result = await movieApiService.SearchByIdAsync(id);
            recentMoviesStorage.AddMovie(result);
            return View(result);
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
