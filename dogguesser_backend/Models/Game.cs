

namespace dogguesser_backend.Models;

public class Game
{
    public List<Round> game;

    public Game(List<Round> rounds)
    {
        game = rounds;
    }
}