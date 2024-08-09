namespace dogguesser_backend.Auth
{
    public interface IAuthService
    {
        string GenerateToken(UserToken user);
    }
}