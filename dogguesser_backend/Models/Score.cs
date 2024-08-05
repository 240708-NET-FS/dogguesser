using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Models;
public class Score
{
    public int ScoreID { get; set; }
    public int UserID { get; set; }
    public DateTime Date { get; set; }
    public float ScoreValue { get; set; }

    public User User { get; set; }

    

}