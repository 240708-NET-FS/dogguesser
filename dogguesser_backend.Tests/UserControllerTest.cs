using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using dogguesser_backend.Controllers;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Service;

namespace dogguesser_backend.Tests
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserService> _userServiceMock;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task GetUserById_ReturnsOkResult_WhenUserIsFound()
        {
            // Arrange
            var userId = 1;
            var user = new UserDTO { UserID = userId, Username = "TestUser" };
            _userServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(userId, returnedUser.UserID);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userId = 1;
            _userServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync((UserDTO)null);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreatedAtActionResult_WhenUserIsCreated()
        {
            // Arrange
            var userDTO = new UserDTO { Username = "NewUser" };
            var createdUser = new UserDTO { UserID = 1, Username = "NewUser" };
            _userServiceMock.Setup(s => s.CreateUserAsync(userDTO)).ReturnsAsync(createdUser);

            // Act
            var result = await _controller.CreateUser(userDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedUser = Assert.IsType<UserDTO>(createdAtActionResult.Value);
            Assert.Equal(createdUser.UserID, returnedUser.UserID);
        }

        [Fact]
        public async Task CreateUser_ReturnsBadRequest_WhenUserDataIsNull()
        {
            // Act
            var result = await _controller.CreateUser(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User data is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ReturnsOkResult_WhenUserIsUpdated()
        {
            // Arrange
            var userDTO = new UserDTO { UserID = 1, Username = "UpdatedUser" };
            _userServiceMock.Setup(s => s.UpdateUserAsync(userDTO)).ReturnsAsync(userDTO);

            // Act
            var result = await _controller.UpdateUser(userDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(userDTO.UserID, returnedUser.UserID);
        }

        [Fact]
        public async Task UpdateUser_ReturnsBadRequest_WhenUserDataIsNull()
        {
            // Act
            var result = await _controller.UpdateUser(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User data is null", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenUserIsDeleted()
        {
            // Arrange
            var userId = 1;
            _userServiceMock.Setup(s => s.DeleteUserAsync(userId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userId = 1;
            _userServiceMock.Setup(s => s.DeleteUserAsync(userId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
