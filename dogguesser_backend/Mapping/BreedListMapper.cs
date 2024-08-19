using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Mapping;

public static class BreedListMapper
{
    public static List<string> ToBreedList(this DogApiBreedList dogApiBreedList)
    {
        Dictionary<string, List<string>> message = dogApiBreedList.Message;

        List<string> breedList = new();

        foreach(KeyValuePair<string, List<string>> superBreed in message)
        {
            if (superBreed.Value.Count == 0){
                breedList.Add(superBreed.Key);
            } 
            else
            {
                switch (superBreed.Key)
                {
                    case "finnish":
                    case "danish":
                    case "australian":
                        foreach(string dog in superBreed.Value)
                        {
                            breedList.Add($"{superBreed.Key} {dog}");
                        }
                        break;
                    default:
                        foreach(string dog in superBreed.Value)
                        {
                            breedList.Add($"{dog} {superBreed.Key}");
                        }
                        break;
                }
            }
        }

        return breedList;
    }
}