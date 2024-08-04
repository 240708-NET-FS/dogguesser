// DTOs/ScoreDTO.cs
namespace dogguesser_backend.DTO;

    public class ScoreDTO
    {
        public int ScoreID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public float ScoreValue { get; set; }
    }

