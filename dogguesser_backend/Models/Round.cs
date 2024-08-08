namespace dogguesser_backend.Models;

public class Round
{
    public int number;
    public string imageUrl;
    public string correctAnswer;

    public Round(int roundNumber, string image, string answer)
    {
        number = roundNumber;
        imageUrl = image;
        correctAnswer = answer;
    }
}