using System;
using Xunit;
using dogguesser_backend.Models;

namespace dogguesser_backend.Tests
{
    public class UserTests
    {
        [Fact]
        public void User_DefaultValues()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.Equal(0, user.UserID); // Default value for int
            Assert.Null(user.Username); // Default value for string
            Assert.Equal(string.Empty, user.Password); // Default value for string
            Assert.False(user.AdmUser); // Default value for bool
        }

        [Fact]
        public void User_SetAndGet_Username()
        {
            // Arrange
            var user = new User();
            var testUsername = "testuser";

            // Act
            user.Username = testUsername;

            // Assert
            Assert.Equal(testUsername, user.Username);
        }

        [Fact]
        public void User_SetAndGet_Password()
        {
            // Arrange
            var user = new User();
            var testPassword = "testpassword";

            // Act
            user.Password = testPassword;

            // Assert
            Assert.Equal(testPassword, user.Password);
        }

        [Fact]
        public void User_SetAndGet_AdmUser()
        {
            // Arrange
            var user = new User();
            var testAdmUser = true;

            // Act
            user.AdmUser = testAdmUser;

            // Assert
            Assert.Equal(testAdmUser, user.AdmUser);
        }

        [Fact]
        public void User_UserID_DefaultValue()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.Equal(0, user.UserID); // Default value for int
        }
    }
}
