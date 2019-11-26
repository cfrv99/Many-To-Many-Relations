using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MoviesApiClient.Models;
using MoviesApiClient.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesApiClient.Services
{
    public class MovieApiService : IMovieApiService
    {
        private HttpClient httpClient;
        private readonly string apiKey;
        private readonly string baseUrl;
        private readonly MovieApiOptions movieApiOptions;
        private readonly IMemoryCache memoryCache;

        public MovieApiService(IHttpClientFactory httpClientFactory, IOptions<MovieApiOptions> options, IMemoryCache memoryCache)
        {
            movieApiOptions = options.Value;
            httpClient = httpClientFactory.CreateClient();
            baseUrl = movieApiOptions.BaseUrl;
            apiKey = movieApiOptions.ApiKey;
            this.memoryCache = memoryCache;
        }

        public async Task<MovieApiResponse> SearchByTitleAsync(string title, int page = 1)
        {
            title = title.ToLower();

            MovieApiResponse result;
            if (!memoryCache.TryGetValue($"{title}_page{page}", out result))
            {
                var response = await httpClient.GetAsync($"{baseUrl}?apikey={apiKey}&s={title}&page={page}");
                var json = await response.Content.ReadAsStringAsync();
                //result = JsonConvert.DeserializeObject<MovieApiResponse>(json);
                result = JsonSerializer.Deserialize<MovieApiResponse>(json);

                if (result.Response == "False")
                    throw new Exception(result.Error);

                memoryCache.Set($"{title}_page{page}", result);
            }
            return result;
        }

        public async Task<Movie> SearchByIdAsync(string id)
        {
            Movie result;
            if (!memoryCache.TryGetValue(id, out result))
            {
                var response = await httpClient.GetAsync($"{baseUrl}?apikey={apiKey}&i={id}");
                var json = await response.Content.ReadAsStringAsync();
                //result = JsonConvert.DeserializeObject<Movie>(json);
                result = JsonSerializer.Deserialize<Movie>(json);

                if (result.Response == "False")
                    throw new Exception(result.Error);

                memoryCache.Set(id, result);
            }
            return result;
        }
    }
}
