using System.Threading.Tasks;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
namespace dogguesser_backend.Service
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task<UserDTO> UpdateUserAsync(UserDTO userDTO);
        Task<bool> DeleteUserAsync(int userId);
         Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
    }
}