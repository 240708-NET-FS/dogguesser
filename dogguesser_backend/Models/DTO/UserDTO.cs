using System.ComponentModel.DataAnnotations;
namespace dogguesser_backend.Models
{
    public class UserDTO
    {
       
        public int UserID { get; set; } // Add this property if it is needed
         public bool AdmUser { get; set; } // This could represent roles like "admin" or "user"
        public string Username { get; set; }
        public string Password { get; set; } 

       
       
    }
}