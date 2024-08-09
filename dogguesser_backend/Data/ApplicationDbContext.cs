using Microsoft.EntityFrameworkCore;
using dogguesser_backend.Models;

namespace dogguesser_backend.Data;

public class ApplicationDbContext : DbContext
{
   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }



    public DbSet<User> Users { get; set; }
    public DbSet<Score> Scores{get;set;}


}


