using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyCookBookApp.Models;
namespace MyCookBookApp.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;
        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient = "http://localhost:5180/api/recipe";
        }
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5180/api/recipe");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Recipe>>(json);
        }
    }
}