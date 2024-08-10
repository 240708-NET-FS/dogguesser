using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using dogguesser_backend.Service;
using dogguesser_backend.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using dogguesser_backend.Controllers;

namespace dogguesser_backend.Tests.Controllers
{
    public class ScoreControllerTests
    {
        private readonly Mock<IScoreService> _mockScoreService;
        private readonly ScoreController _controller;

        public ScoreControllerTests()
        {
            _mockScoreService = new Mock<IScoreService>();
            _controller = new ScoreController(_mockScoreService.Object);
        }

        [Fact]
        public async Task SubmitScore_ValidScore_ReturnsOkResult()
        {
            // Arrange
            var scoreDTO = new ScoreDTO { UserID = 1, ScoreValue = 100 };
            var submittedScore = new ScoreDTO { UserID = 1, ScoreValue = 100 };
            _mockScoreService.Setup(service => service.SubmitScoreAsync(scoreDTO)).ReturnsAsync(submittedScore);

            // Act
            var result = await _controller.SubmitScore(scoreDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ScoreDTO>(okResult.Value);
            Assert.Equal(submittedScore.UserID, returnValue.UserID);
            Assert.Equal(submittedScore.ScoreValue, returnValue.ScoreValue);
        }

       // [Fact]
        public async Task SubmitScore_InvalidScore_ReturnsBadRequest()
        {
            // Arrange
            ScoreDTO scoreDTO = null; // This will be checked in your controller

            // Act
            var result = await _controller.SubmitScore(scoreDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Score data is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetLeaderboard_ReturnsOkResult()
        {
            // Arrange
            var leaderboard = new List<ScoreDTO>
            {
                new ScoreDTO { UserID = 1, ScoreValue = 100 },
                new ScoreDTO { UserID = 2, ScoreValue = 200 }
            };
            _mockScoreService.Setup(service => service.GetLeaderboardAsync()).ReturnsAsync(leaderboard);

            // Act
            var result = await _controller.GetLeaderboard();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ScoreDTO>>(okResult.Value);
            Assert.Equal(leaderboard.Count, returnValue.Count);
            Assert.Contains(returnValue, score => score.UserID == 1 && score.ScoreValue == 100);
            Assert.Contains(returnValue, score => score.UserID == 2 && score.ScoreValue == 200);
        }
    }
}
