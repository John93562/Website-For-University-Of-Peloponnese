using DeluzionalPenguinz.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DeluzionalPenguinz.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<Human>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Anouncement> Anouncements { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


    }
}
