using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApiClient.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
    }
}
