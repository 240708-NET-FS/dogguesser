namespace dogguesser_backend.Models.DTO;

public class DogApiBreedList
{
    public Dictionary<string, List<string>> Message {get; set;}
    public string Status {get; set;}
}