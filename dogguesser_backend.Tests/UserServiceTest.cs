using Microsoft.EntityFrameworkCore;
using Xunit;
using dogguesser_backend.Data;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace dogguesser_backend.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;

        public UserServiceTests()
        {
            // Use a unique in-memory database name for each test run
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            _context = new ApplicationDbContext(options);

            // Seed the database with test data
            SeedDatabase();
            
            _userService = new UserService(_context);
        }

        private void SeedDatabase()
        {
            var users = new List<User>
            {
                new User { UserID = 1, Username = "testuser1", Password = "hashedpassword1", AdmUser = false },
                new User { UserID = 2, Username = "testuser2", Password = "hashedpassword2", AdmUser = true }
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUserDTO()
        {
            // Arrange
            int userId = 1;

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserID);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldCreateAndReturnUserDTO()
        {
            // Arrange
            var userDTO = new UserDTO
            {
                UserID = 3,
                Username = "newuser",
                Password = "newpassword",
                AdmUser = false
            };

            // Act
            var result = await _userService.CreateUserAsync(userDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userDTO.Username, result.Username);
            Assert.Equal(userDTO.AdmUser, result.AdmUser);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateAndReturnUserDTO()
        {
            // Arrange
            var userDTO = new UserDTO
            {
                UserID = 1,
                Username = "updateduser",
                AdmUser = true
            };

            // Act
            var result = await _userService.UpdateUserAsync(userDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userDTO.Username, result.Username);
            Assert.Equal(userDTO.AdmUser, result.AdmUser);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnTrueIfUserExists()
        {
            // Arrange
            int userId = 1;

            // Act
            var result = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnFalseIfUserDoesNotExist()
        {
            // Arrange
            int userId = 999;

            // Act
            var result = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.False(result);
        }
    }
}
