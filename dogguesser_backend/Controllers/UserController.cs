using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dogguesser_backend.Service;
using dogguesser_backend.Models;
using dogguesser_backend.Models.DTO;
//using Microsoft.AspNetCore.Authorization;

namespace dogguesser_backend.Controllers
{
    [ApiController]
    
    //[Authorize] // Ensure all endpoints require authentication
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUserByID")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch
            {
                // Optionally log the exception
                return StatusCode(500, "Internal server error");
            }
        }
  
    [HttpPost("api/register")]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
    {
        if (userDTO == null)
        {
            return BadRequest("User data is required.");
        }

        try
        {
            // Call the service to create the user
            var createdUser = await _userService.CreateUserAsync(userDTO);
            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserID }, createdUser);
        }
        catch (ArgumentException ex)
        {
            // Return a 400 Bad Request with the validation error message
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception if needed and return a 500 Internal Server Error
            // Example: use a logging framework here
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user.");
        }
    }


        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User data is null");
            }

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(userDTO);
                return Ok(updatedUser);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                // Optionally log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(userId);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch
            {
                // Optionally log the exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
