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
                    var responseBody = await content.ReadAsStringAsync();
                    
                    string status = responseBody.Split("status\":\"")[1].Split("\"")[0];

                    string messageFromResponse = responseBody.Split("},")[0].Split(":{")[1];
                    string cleanMessage = messageFromResponse.Replace("\"", "");
                    //Console.WriteLine(cleanMessage);

                    string[] breedsAsArray = cleanMessage.Split("],");

                    Dictionary<string, List<string>> dogBreedDict = [];

                    foreach (string str in breedsAsArray)
                    {
                        string[] halves = str.Split(":");
                        string key = halves[0].Trim();
                        if(halves[1].Length < 2){
                            dogBreedDict.Add(key, []);
                        }
                        else
                        {
                            string[] valuesArray = halves[1].Split("[")[1].Split(","); 
                            List<string> valuesList = [];
                            foreach(string value in valuesArray)
                            {
                                valuesList.Add(value.Trim());
                            }
                            dogBreedDict.Add(key, valuesList);
                        }
                    }
                    DogApiBreedList dogApiBreedList = new(){Message = dogBreedDict, Status=status};

                    return dogApiBreedList;
                }
            }
        }
        
    }

    public static KeyValuePair<string, List<string>> CleanString(string str)
    {
        string[] halves = str.Split(":");
        string key = halves[0];
        if(halves[1].Length > 1){
            KeyValuePair<string, List<string>> dictItem = new(key, []);
            return dictItem;
        }
        else
        {
            string[] valuesArray = halves[1].Split("[")[1].Split(","); 
            List<string> valuesList = [];
            foreach(string value in valuesArray)
            {
                valuesList.Add(value);
            }
            KeyValuePair<string, List<string>> dictItem = new(key, valuesList);
            return dictItem;
        }
    }

}