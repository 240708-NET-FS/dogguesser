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
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]  UserLoginModel model)
        {

            var dbUser = await _userService.GetUserByUsernameAndPasswordAsync(model.Username, model.Password);


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