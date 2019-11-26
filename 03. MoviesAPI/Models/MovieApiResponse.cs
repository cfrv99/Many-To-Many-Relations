//using Newtonsoft.Json;
using MoviesApiClient.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesApiClient.Models
{

    public class MovieApiResponse
    {
        [JsonPropertyName("Search")]
        public IEnumerable<Movie> Movies { get; set; }

        [JsonPropertyName("totalResults")]
        [JsonConverter(typeof(IntJsonConverter))]
        public int TotalResults { get; set; }

        public string Response { get; set; }

        public string Error { get; set; }
    }
}




//public int TotalResults { get => Int32.Parse(TotalResultsString); }