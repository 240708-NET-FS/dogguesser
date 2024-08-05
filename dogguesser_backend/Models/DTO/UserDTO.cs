using System.ComponentModel.DataAnnotations;
namespace dogguesser_backend.Models
{
    public class UserDTO
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }=string.Empty;
        public bool AdmUser { get; set; } // This could represent roles like "admin" or "user"
    }
}