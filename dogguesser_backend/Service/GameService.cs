using dogguesser_backend.Models.DTO;
using System.Text.Json;

namespace dogguesser_backend.Service;

public class GameService : IGameService
{
    public async Task<DogApiImages> GetImages()
    {
        string dogImagesUrl = "https://dog.ceo/api/breeds/image/random/10";
        
        HttpClient client = new();

        using HttpResponseMessage response = await client.GetAsync(dogImagesUrl);

        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        var data = JsonSerializer.Deserialize<DogApiImages>(responseBody);
        return data; 
    }
}