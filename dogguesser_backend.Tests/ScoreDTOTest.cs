using System;
using Xunit;
using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Tests
{
    public class ScoreDTOTests
    {
        [Fact]
        public void ScoreDTO_ShouldInstantiateCorrectly()
        {
            // Arrange
            var scoreId = 1;
            var userId = 2;
            var date = DateTime.Now;
            var scoreValue = 99.9f;

            // Act
            var scoreDTO = new ScoreDTO
            {
                ScoreID = scoreId,
                UserID = userId,
                Date = date,
                ScoreValue = scoreValue
            };

            // Assert
            Assert.Equal(scoreId, scoreDTO.ScoreID);
            Assert.Equal(userId, scoreDTO.UserID);
            Assert.Equal(date, scoreDTO.Date);
            Assert.Equal(scoreValue, scoreDTO.ScoreValue);
        }

        [Fact]
        public void ScoreDTO_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var scoreDTO = new ScoreDTO();

            // Act
            scoreDTO.ScoreID = 10;
            scoreDTO.UserID = 20;
            scoreDTO.Date = new DateTime(2024, 8, 7);
            scoreDTO.ScoreValue = 88.8f;

            // Assert
            Assert.Equal(10, scoreDTO.ScoreID);
            Assert.Equal(20, scoreDTO.UserID);
            Assert.Equal(new DateTime(2024, 8, 7), scoreDTO.Date);
            Assert.Equal(88.8f, scoreDTO.ScoreValue);
        }
    }
}
