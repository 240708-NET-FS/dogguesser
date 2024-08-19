using System.ComponentModel.DataAnnotations;
namespace dogguesser_backend.Models
{
    public class UserDTO
    {
       
<<<<<<< HEAD

         public bool AdmUser { get; set; } // This could represent roles like "admin" or "user"
        public string Username { get; set; }
        public string Password { get; set; }=string.Empty; 

=======
        public int UserID { get; set; } // Add this property if it is needed
         public bool AdmUser { get; set; } // This could represent roles like "admin" or "user"
        public string Username { get; set; }
        public string Password { get; set; } 

>>>>>>> a7b59e44616e1cc69ead37be8ae3f307c5b607e4
       
       
    }
}