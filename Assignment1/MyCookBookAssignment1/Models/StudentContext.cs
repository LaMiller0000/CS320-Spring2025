using Microsoft.EntityFrameworkCore;

namespace MyCookBookAssignment1.Models
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentContext(DbContextOptions options) : base(options)
        {
            return;
        }
    }
}
