using MoviesApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiClient.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public string Title { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
