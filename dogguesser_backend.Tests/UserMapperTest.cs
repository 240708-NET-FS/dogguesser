using Xunit;
using dogguesser_backend.Mapping;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
using System;

namespace dogguesser_backend.Tests
{
    public class UserMapperTests
    {
        [Fact]
        public void ToDTO_ShouldMapUserToUserDTO()
        {
            // Arrange
            var user = new User
            {
                UserID = 1,
                Username = "testuser",
                AdmUser = true,
                Password = "securepassword"
            };

            // Act
            var result = user.ToDTO();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
           
            Assert.Equal(user.AdmUser, result.AdmUser);
            // Note: Password is not included in DTO, so it's not asserted here
        }

        [Fact]
        public void ToEntity_ShouldMapUserDTOToUser()
        {
            // Arrange
            var userDTO = new UserDTO
            {
                UserID = 1,
                Username = "testuser",
                AdmUser = true,
                Password = "securepassword" // This password will be mapped
            };

            // Act
            var result = userDTO.ToEntity();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userDTO.UserID, result.UserID);
            
            Assert.Equal(userDTO.AdmUser, result.AdmUser);
            Assert.Equal(userDTO.Password, result.Password);
        }

        [Fact]
        public void ToDTO_WithNullUser_ShouldReturnNull()
        {
            // Arrange
            User user = null;

            // Act
            var result = user.ToDTO();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToEntity_WithNullUserDTO_ShouldReturnNull()
        {
            // Arrange
            UserDTO userDTO = null;

            // Act
            var result = userDTO.ToEntity();

            // Assert
            Assert.Null(result);
        }
    }
}
