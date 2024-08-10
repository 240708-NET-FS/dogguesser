using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using dogguesser_backend.Controllers;
using dogguesser_backend.Auth;
using dogguesser_backend.Models;
using dogguesser_backend.Service;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<IUserService> _mockUserService;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _mockUserService = new Mock<IUserService>();
        _controller = new AuthController(_mockAuthService.Object, _mockUserService.Object);
    }

    //[Fact]
    public async Task Login_ValidUser_ReturnsOkWithToken()
    {
        // Arrange
        var model = new UserLoginModel { Username = "testUser", Password = "testPass" };
        var dbUser = new User
        {
            Username = model.Username,
            Password = model.Password, // Assuming password is stored in plain text for this example
            AdmUser = false,
            UserID = 1
        };
        var token = "sampleToken";

        _mockUserService.Setup(service => service.GetUserByUsernameAndPasswordAsync(model.Username, model.Password))
                        .ReturnsAsync(dbUser);
        _mockAuthService.Setup(service => service.GenerateToken(It.IsAny<UserToken>()))
                        .Returns(token);

        // Act
        var result = await _controller.Login(model);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<Dictionary<string, string>>(actionResult.Value);
        Assert.True(response.ContainsKey("token"));
        Assert.Equal(token, response["token"]);
    }

   [Fact]
    public async Task Login_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        UserLoginModel model = null;

        // Act
        var result = await _controller.Login(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_EmptyUsernameOrPassword_ReturnsBadRequest()
    {
        // Arrange
        var model = new UserLoginModel { Username = "", Password = "" };

        // Act
        var result = await _controller.Login(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var model = new UserLoginModel { Username = "testUser", Password = "wrongPass" };
        _mockUserService.Setup(service => service.GetUserByUsernameAndPasswordAsync(model.Username, model.Password))
                        .ReturnsAsync((User)null);

        // Act
        var result = await _controller.Login(model);

        // Assert
        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}
