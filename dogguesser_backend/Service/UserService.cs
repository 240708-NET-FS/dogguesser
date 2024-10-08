using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
using dogguesser_backend.Data;
<<<<<<< HEAD
using BCrypt.Net; 
=======
using dogguesser_backend.Hashing;
using BCrypt.Net;
>>>>>>> a7b59e44616e1cc69ead37be8ae3f307c5b607e4

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

<<<<<<< HEAD
       public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
{
    if (userDTO == null)
        throw new ArgumentNullException(nameof(userDTO));
=======
        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ArgumentNullException(nameof(userDTO));
>>>>>>> a7b59e44616e1cc69ead37be8ae3f307c5b607e4

            // Perform additional validation if needed
            if (string.IsNullOrEmpty(userDTO.Username))
                throw new ArgumentException("Username is required", nameof(userDTO));

            if (string.IsNullOrEmpty(userDTO.Password))
                throw new ArgumentException("Password is required", nameof(userDTO));

            // Hash the password
            var hashedPassword = PasswordHelper.HashPassword(userDTO.Password);

<<<<<<< HEAD
    return UserMapper.ToDTO(user);
}       public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
=======
            // Map DTO to entity
            var user = UserMapper.ToEntity(userDTO);
            user.Password = hashedPassword; // Set the hashed password

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

            // Map entity back to DTO
            return UserMapper.ToDTO(user);
        }
        public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
>>>>>>> a7b59e44616e1cc69ead37be8ae3f307c5b607e4
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

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == PasswordHelper.HashPassword(password));
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
