using System;
using Xunit;
using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Tests
{
    public class UserDTOTests
    {
        [Fact]
        public void UserDTO_ShouldInstantiateCorrectly()
        {
            // Arrange
            var userId = 1;
            var username = "testuser";
            var password = "password";
            var admUser = true;

            // Act
            var userDTO = new UserDTO
            {
                UserID = userId,
                Username = username,
                Password = password,
                AdmUser = admUser
            };

            // Assert
            Assert.Equal(userId, userDTO.UserID);
            Assert.Equal(username, userDTO.Username);
            Assert.Equal(password, userDTO.Password);
            Assert.Equal(admUser, userDTO.AdmUser);
        }

        [Fact]
        public void UserDTO_ShouldSetAndGetPropertiesCorrectly()
        {
            // Arrange
            var userDTO = new UserDTO();

            // Act
            userDTO.UserID = 2;
            userDTO.Username = "newuser";
            userDTO.Password = "newpassword";
            userDTO.AdmUser = false;

            // Assert
            Assert.Equal(2, userDTO.UserID);
            Assert.Equal("newuser", userDTO.Username);
            Assert.Equal("newpassword", userDTO.Password);
            Assert.False(userDTO.AdmUser);
        }
    }
}
