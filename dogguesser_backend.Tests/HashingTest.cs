using Xunit;
using dogguesser_backend.Hashing;

namespace dogguesser_backend.Tests
{
    public class PasswordHelperTests
    {
        //[Fact]
        public void HashPassword_ValidPassword_ReturnsExpectedHash()
        {
            // Arrange
            var password = "mySecurePassword123";
            var expectedHash = "a1b2c3d4e5f6g7h8i9j0klmnopqrstuvwxyz0123456789"; // Replace with the actual expected hash

            // Act
            var actualHash = PasswordHelper.HashPassword(password);

            // Assert
            Assert.Equal(expectedHash, actualHash);
        }

        [Fact]
        public void HashPassword_DifferentPasswords_ReturnDifferentHashes()
        {
            // Arrange
            var password1 = "password1";
            var password2 = "password2";

            // Act
            var hash1 = PasswordHelper.HashPassword(password1);
            var hash2 = PasswordHelper.HashPassword(password2);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void HashPassword_SamePassword_ReturnsSameHash()
        {
            // Arrange
            var password = "myPassword";

            // Act
            var hash1 = PasswordHelper.HashPassword(password);
            var hash2 = PasswordHelper.HashPassword(password);

            // Assert
            Assert.Equal(hash1, hash2);
        }
    }
}
