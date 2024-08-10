using Xunit;
using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Tests
{
    public class LoginDTOTests
    {
        [Fact]
        public void LoginDTO_ShouldInstantiateCorrectly()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";

            // Act
            var loginDTO = new LoginDTO
            {
                Username = username,
                Password = password
            };

            // Assert
            Assert.Equal(username, loginDTO.Username);
            Assert.Equal(password, loginDTO.Password);
        }

        [Fact]
        public void LoginDTO_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var loginDTO = new LoginDTO();

            // Act
            loginDTO.Username = "newuser";
            loginDTO.Password = "newpassword";

            // Assert
            Assert.Equal("newuser", loginDTO.Username);
            Assert.Equal("newpassword", loginDTO.Password);
        }
    }
}
