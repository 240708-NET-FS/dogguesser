using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dogguesser_backend.Models;
using dogguesser_backend.Data;
using BCrypt.Net; 

namespace dogguesser_backend.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserID == userId);

            return UserMapper.ToDTO(user);
        }

       public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
{
    if (userDTO == null)
        throw new ArgumentNullException(nameof(userDTO));

    // Perform additional validation if needed
    if (string.IsNullOrEmpty(userDTO.Username))
        throw new ArgumentException("Username is required", nameof(userDTO));

    var user = UserMapper.ToEntity(userDTO);

    try
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        // Log the exception and handle it as needed
        throw new InvalidOperationException("An error occurred while creating the user.", ex);
    }

    return UserMapper.ToDTO(user);
}       public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ArgumentNullException(nameof(userDTO));

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserID == userDTO.UserID);

            if (user == null)
                throw new InvalidOperationException("User not found");

            user.Username = userDTO.Username;
            user.AdmUser = userDTO.AdmUser;
            // Update additional fields as necessary

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return UserMapper.ToDTO(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
