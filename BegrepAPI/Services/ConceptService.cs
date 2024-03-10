using BegrepAPI.Contracts;
using BegrepAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace BegrepAPI.Services
{
    public class ConceptService : IConceptService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ConceptService> _logger;

        public ConceptService(HttpClient client, ILogger<ConceptService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<List<Concept>> GetAllConceptsAsync(int page)
        {
            _logger.LogInformation("Fetching concepts from Felles Datakatalog");
            var requestBody = new { page = page };
            var response = await _client.PostAsJsonAsync("concepts", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var concepts = JsonConvert.DeserializeObject<List<Concept>>(responseString);
                return concepts;
            }

            throw new Exception($"Failed to fetch concepts. Status code: {response.StatusCode}");
        }

        public async Task<Concept> GetConceptAsync(string id)
        {
            _logger.LogInformation($"Fetching concept with ID {id} from Felles Datakatalog");
            var response = await _client.GetAsync($"concepts/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Concept>(responseString);
            }

            throw new Exception($"Failed to fetch concept with ID {id}. Status code: {response.StatusCode}");
        }
    }
}
