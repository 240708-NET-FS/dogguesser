using dogguesser_backend.Data;
using dogguesser_backend.Mapping;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Service;

namespace dogguesser_backend.tests;

public class BreedServiceTests
{
    [Fact]
    public void BreedMappingWords()
    {
        List<string> goal = new();
        goal.Add("australian kelpie");
        goal.Add("australian shepherd");
        goal.Add("great dane");
        goal.Add("basset hound");
        goal.Add("blood hound");
        goal.Add("husky");

        Dictionary<string, List<string>> startDict = new();
        startDict.Add("australian", ["kelpie", "shepherd"]);
        startDict.Add("dane", ["great"]);
        startDict.Add("hound", ["basset", "blood"]);
        startDict.Add("husky", []);

        DogApiBreedList dogApiBreedList = new DogApiBreedList(){Message=startDict, Status="goodIGuess"};

        List<string> result = dogApiBreedList.ToBreedList();

        Assert.Equal(goal, result);
    }
}