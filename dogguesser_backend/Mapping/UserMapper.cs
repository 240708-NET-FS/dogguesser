// Mapping/UserMapper.cs
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;

namespace dogguesser_backend.Mapping
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this User user)
        {
            if (user == null)
                return null;

            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                AdmUser = user.AdmUser
                // Note: Password is not included in UserDTO
            };
        }

        public static User ToEntity(this UserDTO userDTO)
        {
            if (userDTO == null)
                return null;

            return new User
            {
                UserID = userDTO.UserID,
                Username = userDTO.Username,
                AdmUser = userDTO.AdmUser,
                Password = userDTO.Password
            };
        }
    }
}
