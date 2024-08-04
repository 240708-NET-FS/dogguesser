using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dogguesser_backend.Models;
public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } 
}