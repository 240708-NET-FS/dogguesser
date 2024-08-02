namespace dogguesser_backend.DTO;

public class Score
{
    public int UserId {get; set;} = null;
    public DateTime Date {get; set; } = null;
    public float Score {get; set;} = null;
}