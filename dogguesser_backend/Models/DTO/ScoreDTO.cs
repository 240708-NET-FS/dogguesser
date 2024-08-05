// DTOs/ScoreDTO.cs
using dogguesser_backend.Models;
namespace dogguesser_backend.Models.DTO;

    public class ScoreDTO
    {
        public int ScoreID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public float ScoreValue { get; set; }

      
    }

