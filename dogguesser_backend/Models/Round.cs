namespace dogguesser_backend.Models;

public class Round
{
    public int number{get; set;}
    public string imageUrl{get; set;}
    public string correctAnswer{get; set;}

    public Round(int roundNumber, string image, string answer)
    {
        number = roundNumber;
        imageUrl = image;
        correctAnswer = answer;
    }
}