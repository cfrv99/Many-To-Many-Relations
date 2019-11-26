using Microsoft.AspNetCore.Mvc;
using MoviesApiClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiClient.ViewComponents
{
    // Home/Search?title=Matrix&page=3
    // Home/Search?title=Matrix&year=2000&page=3
    // News/Posts?type=sport&orderby=likes&page=3

    public class PaginationViewComponent : ViewComponent
    {
        //public IViewComponentResult Invoke();
        //public async Task<IViewComponentResult> InvokeAsync();

        public IViewComponentResult Invoke(
            int totalPages, 
            int currentPage, 
            string controller, 
            string action,
            Dictionary<string, string> routeParams)
        {
            var model = new PaginationViewModel
            {
                TotalPages = totalPages,
                CurrentPage = currentPage,
                Controller = controller,
                Action = action,
                RouteParams = routeParams
            };

            return View("New", model);
        }
    }
}
