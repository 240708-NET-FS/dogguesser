using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Mapping;

public static class GameMapper
{
    public static Game ToGame(this DogApiImages dogApiImages)
    {
        List<string> message = dogApiImages.Message;

        List<Round> rounds = new();

        for(int i = 0; i < 10; i++)
        {
            string correctAnswer;
            int number = i+1;
            string image = message[i].Replace("\\", "");

            // I hardcode 4 here because of the returned string
            // An example string is: "https://images.dog.ceo/breeds/corgi-cardigan/n02113186_5833.jpg
            // Splitting on "/" and getting the 4th element to get corgi-cardigan
            string answer = image.Split("/")[4];
            if (answer.Contains("-"))
            {
                string[] split = answer.Split('-');
                string precedent = split[1];
                string subsequent = split[0];
                correctAnswer = $"{precedent} {subsequent}";
            }
            else
            {
                correctAnswer = answer;
            }

            Round newRound = new Round(number, image, correctAnswer);
            rounds.Add(newRound);
        }
        Game newGame = new Game(rounds);
        return newGame;
    }
}