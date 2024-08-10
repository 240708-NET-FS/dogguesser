using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using dogguesser_backend.Controllers;
using dogguesser_backend.Service;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }

    [Fact]
    public async Task GetUserById_UserExists_ReturnsOkResultWithUser()
    {
        // Arrange
        var userId = 1;
        var user = new UserDTO { UserID = userId, Username= "Test User" };
        _mockUserService.Setup(service => service.GetUserByIdAsync(userId))
                        .ReturnsAsync(user);

        // Act
        var result = await _controller.GetUserById(userId);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnUser = Assert.IsType<UserDTO>(actionResult.Value);
        Assert.Equal(userId, returnUser.UserID);
    }

    [Fact]
    public async Task GetUserById_UserDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(service => service.GetUserByIdAsync(userId))
                        .ReturnsAsync((UserDTO)null);

        // Act
        var result = await _controller.GetUserById(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateUser_ValidUser_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var userDTO = new UserDTO { UserID = 1, Username = "Test User" };
        _mockUserService.Setup(service => service.CreateUserAsync(userDTO))
                        .ReturnsAsync(userDTO);

        // Act
        var result = await _controller.CreateUser(userDTO);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdUser = Assert.IsType<UserDTO>(actionResult.Value);
        Assert.Equal(userDTO.UserID, createdUser.UserID);
    }

    [Fact]
    public async Task CreateUser_NullUserDTO_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.CreateUser(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUser_ValidUser_ReturnsOkResult()
    {
        // Arrange
        var userDTO = new UserDTO { UserID = 1, Username = "Updated User" };
        _mockUserService.Setup(service => service.UpdateUserAsync(userDTO))
                        .ReturnsAsync(userDTO);

        // Act
        var result = await _controller.UpdateUser(userDTO);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var updatedUser = Assert.IsType<UserDTO>(actionResult.Value);
        Assert.Equal(userDTO.UserID, updatedUser.UserID);
    }

    [Fact]
    public async Task UpdateUser_NullUserDTO_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.UpdateUser(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUser_UserExists_ReturnsNoContent()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(service => service.DeleteUserAsync(userId))
                        .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUser_UserDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(service => service.DeleteUserAsync(userId))
                        .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
