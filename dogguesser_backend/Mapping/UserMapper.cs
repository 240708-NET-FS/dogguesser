using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Models
{
    public static class UserMapper
    {
        // Method to map User to UserDTO
        public static UserDTO ToDTO(User user)
        {
            if (user == null)
            {
                return null;
            }
            
            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                AdmUser = user.AdmUser,
                Password =user.Password
            };
        }

        // Method to map UserDTO to User
        public static User ToEntity(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return null;
            }
            
            return new User
            {
                UserID = userDTO.UserID,
                Username = userDTO.Username,
                AdmUser = userDTO.AdmUser,
                Password = userDTO.Password
                // Note: Password is not included in DTO, so it needs to be set separately
            };
        }
    }
}
