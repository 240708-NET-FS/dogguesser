namespace dogguesser_backend.Auth;

public static class AuthSettings
{
  public static string PrivateKey { get; set; } = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
}

public class UserToken  {
        public int UserID { get; set; }
        public string Username { get; set; }  
        public bool AdmUser { get; set; }

}