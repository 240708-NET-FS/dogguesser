using Xunit;
using dogguesser_backend.Mapping;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
using System;

namespace dogguesser_backend.Tests
{
    public class ScoreMapperTests
    {
        [Fact]
        public void ToDTO_ShouldMapScoreToScoreDTO()
        {
            // Arrange
            var score = new Score
            {
                ScoreID = 1,
                UserID = 123,
                Date = new DateTime(2024, 8, 9),
                ScoreValue = 90
            };

            // Act
            var result = score.ToDTO();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(score.ScoreID, result.ScoreID);
            Assert.Equal(score.UserID, result.UserID);
            Assert.Equal(score.Date, result.Date);
            Assert.Equal(score.ScoreValue, result.ScoreValue);
        }

        [Fact]
        public void ToEntity_ShouldMapScoreDTOToScore()
        {
            // Arrange
            var scoreDTO = new ScoreDTO
            {
                ScoreID = 1,
                UserID = 123,
                Date = new DateTime(2024, 8, 9),
                ScoreValue = 90
            };

            // Act
            var result = scoreDTO.ToEntity();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(scoreDTO.ScoreID, result.ScoreID);
            Assert.Equal(scoreDTO.UserID, result.UserID);
            Assert.Equal(scoreDTO.Date, result.Date);
            Assert.Equal(scoreDTO.ScoreValue, result.ScoreValue);
        }

        [Fact]
        public void ToDTO_WithNullScore_ShouldReturnNull()
        {
            // Arrange
            Score score = null;

            // Act
            var result = score.ToDTO();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToEntity_WithNullScoreDTO_ShouldReturnNull()
        {
            // Arrange
            ScoreDTO scoreDTO = null;

            // Act
            var result = scoreDTO.ToEntity();

            // Assert
            Assert.Null(result);
        }
    }
}
