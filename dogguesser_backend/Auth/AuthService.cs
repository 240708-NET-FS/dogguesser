using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dogguesser_backend.Models;

using Microsoft.IdentityModel.Tokens;


namespace dogguesser_backend.Auth;
public class AuthService
{
    public string GenerateToken(UserToken user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(UserToken user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Username));
        claims.AddClaim(new Claim(ClaimTypes.Role, user.AdmUser ? "Admin" : "User"));

        return claims;
    }
}