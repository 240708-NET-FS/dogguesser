using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dogguesser_backend.Models;
public class User
{         
        [Key]
        public int UserID { get; set; }

       
        public string Username { get; set; }

        public string Password { get; set; }=string.Empty; 

        public bool AdmUser { get; set; } // This could represent roles like "admin" or "user"
}