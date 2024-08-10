using dogguesser_backend.Data;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
using Microsoft.EntityFrameworkCore;
using dogguesser_backend.Service;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace dogguesser_backend.Tests
{
    public class ScoreServiceTests
    {
        private readonly ScoreService _scoreService;
        private readonly ApplicationDbContext _context;

        public ScoreServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _scoreService = new ScoreService(_context);

            // Seed the database with initial data using ScoreDTO
            _context.Scores.AddRange(new List<Score>
            {
                new Score { ScoreValue = 100, User = new User { Username = "User1", Password = "password1" } },
                new Score { ScoreValue = 200, User = new User { Username = "User2", Password = "password2" } },
                new Score { ScoreValue = 200, User = new User { Username = "User3", Password = "password3" } },
                new Score { ScoreValue = 200, User = new User { Username = "User4", Password = "password4" } },
                new Score { ScoreValue = 200, User = new User { Username = "User5", Password = "password5" } }
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task SubmitScoreAsync_AddsScoreToDatabase()
        {
            // Arrange
            var newScoreDTO = new ScoreDTO { ScoreValue = 150, UserID = 1 }; // Assume UserID 1 exists

            // Act
            var result = await _scoreService.SubmitScoreAsync(newScoreDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newScoreDTO.ScoreValue, result.ScoreValue);

            
        }

          [Fact]
        public async Task SubmitScoreAsync_AddsScoreToDatabase1()
        {
            // Arrange
            var newScoreDTO = new ScoreDTO { ScoreValue = 250, UserID = 2 }; // Assume UserID 1 exists

            // Act
            var result = await _scoreService.SubmitScoreAsync(newScoreDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newScoreDTO.ScoreValue, result.ScoreValue);

            
        }

        [Fact]
        public async Task GetLeaderboardAsync_ReturnsScoresInDescendingOrder()
        {
            // Act
            var leaderboard = await _scoreService.GetLeaderboardAsync();

            // Assert
            Assert.NotNull(leaderboard);
            Assert.Equal(5, leaderboard.Count);
            Assert.True(leaderboard[0].ScoreValue >= leaderboard[1].ScoreValue);
        }
    }
}
