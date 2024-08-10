using System;
using Xunit;
using dogguesser_backend.Models;

namespace dogguesser_backend.Tests
{
    public class ScoreTests
    {
        [Fact]
        public void Score_Properties_SetAndGetCorrectly()
        {
            // Arrange
            var scoreId = 1;
            var userId = 2;
            var date = DateTime.UtcNow;
            var scoreValue = 95.5f;

            var score = new Score
            {
                ScoreID = scoreId,
                UserID = userId,
                Date = date,
                ScoreValue = scoreValue
            };

            // Act & Assert
            Assert.Equal(scoreId, score.ScoreID);
            Assert.Equal(userId, score.UserID);
            Assert.Equal(date, score.Date);
            Assert.Equal(scoreValue, score.ScoreValue);
        }

        [Fact]
        public void Score_Constructor_SetsDefaultValues()
        {
            // Arrange & Act
            var score = new Score();

            // Assert
            Assert.Equal(0, score.ScoreID); // Default value for int
            Assert.Equal(0, score.UserID);  // Default value for int
            Assert.Equal(default(DateTime), score.Date); // Default value for DateTime
            Assert.Equal(0f, score.ScoreValue); // Default value for float
            Assert.Null(score.User); // Default value for reference types
        }

        [Fact]
        public void Score_User_ReferenceIsNullByDefault()
        {
            // Arrange
            var score = new Score();

            // Act
            var user = score.User;

            // Assert
            Assert.Null(user);
        }
    }
}
