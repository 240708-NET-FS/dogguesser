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

        string status = responseBody.Split("status\":\"")[1].Split("\"}")[0];
        
        string message = responseBody.Split("[")[1].Split("]")[0];
        string newMessage = message.Replace("\"", "");
            
        string[] messageAsArray = newMessage.Split(",");
        List<string> messageAsList = [];
            
            foreach(string img in messageAsArray)
        {
            messageAsList.Add(img);
        }

        DogApiImages dogApiImages = new(){Status=status, Message=messageAsList};

        return dogApiImages;
    }
}