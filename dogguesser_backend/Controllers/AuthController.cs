using Microsoft.AspNetCore.Mvc;
using dogguesser_backend.Auth;
using dogguesser_backend.Models;
using dogguesser_backend.Auth;
using dogguesser_backend.Service;

namespace dogguesser_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]  UserLoginModel model)
        {

            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Invalid login request");
            }

            var dbUser = await _userService.GetUserByUsernameAndPasswordAsync(model.Username, model.Password);

              if (dbUser == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var user = new UserToken
            {
                Username = dbUser.Username,
                AdmUser = dbUser.AdmUser
            };

            var token = _authService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}