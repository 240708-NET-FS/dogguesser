

namespace dogguesser_backend.Models; 

public class Game
{
    public List<Round> rounds{get; set;}

    public Game(List<Round> roundsList)
    {
        rounds = roundsList;
    }
}