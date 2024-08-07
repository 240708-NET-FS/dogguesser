// Mapping/ScoreMapper.cs
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Mapping;

    public static class ScoreMapper
    {
        public static ScoreDTO ToDTO(this Score score)
        {
            return new ScoreDTO
            {
                ScoreID = score.ScoreID,
                UserID = score.UserID,
                Date = score.Date,
                ScoreValue = score.ScoreValue
            };
        }

        public static Score ToEntity(this ScoreDTO scoreDTO)
        {
            return new Score
            {
                ScoreID = scoreDTO.ScoreID,
                UserID = scoreDTO.UserID,
                Date = scoreDTO.Date,
                ScoreValue = scoreDTO.ScoreValue
            };
        }
    }

