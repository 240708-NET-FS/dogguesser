using dogguesser_backend.Data;
using dogguesser_backend.Models;
using Microsoft.EntityFrameworkCore;
using dogguesser_backend.Service;

using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace dogguesser_backend.Tests;

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

            // Seed the database with initial data
            _context.Scores.AddRange(new List<Score>
            {
                new Score { ScoreValue = 100, User = new User { Username = "User1",Password="papa" } },
                new Score { ScoreValue = 200, User = new User { Username = "User2",Password="papa" } }
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task SubmitScoreAsync_AddsScoreToDatabase()
        {
            // Arrange
            var newScore = new Score { ScoreValue = 150, User = new User { Username = "User3",Password="papa" } };

            // Act
            var result = await _scoreService.SubmitScoreAsync(newScore);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newScore.ScoreValue, result.ScoreValue);

            var scores = await _context.Scores.ToListAsync();
            Assert.Contains(scores, s => s.ScoreValue == 150 && s.User.Username == "User3");
        }

        [Fact]
        public async Task GetLeaderboardAsync_ReturnsScoresInDescendingOrder()
        {
            // Act
            var leaderboard = await _scoreService.GetLeaderboardAsync();

            // Assert
            Assert.NotNull(leaderboard);
            Assert.Equal(2, leaderboard.Count);
            Assert.True(leaderboard[0].ScoreValue >= leaderboard[1].ScoreValue);
        }
    }

