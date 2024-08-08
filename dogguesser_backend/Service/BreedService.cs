using dogguesser_backend.Data;
using dogguesser_backend.Mapping;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
namespace dogguesser_backend.Service; 

public class BreedService : IBreedService
{
    public async Task<DogApiBreedList> GetBreedsList()
    {
        // I want to map this to a response object and use the JSON de-serializer
        string dogUrl = "https://dog.ceo/api/breeds/list/all";
        using (HttpClient client = new())
        {
            using (HttpResponseMessage res = await client.GetAsync(dogUrl))
            {
                using (HttpContent content = res.Content)
                {
                    var jData = await content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<DogApiBreedList>(jData);
                    return data;
                }
            }
        }
        
    }

}