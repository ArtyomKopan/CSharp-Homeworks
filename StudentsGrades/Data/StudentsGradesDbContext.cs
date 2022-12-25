using Microsoft.EntityFrameworkCore;

namespace StudentsGrades.Data
{
    public class StudentsGradesDbContext : DbContext
    {
        public StudentsGradesDbContext(
        DbContextOptions<StudentsGradesDbContext> options)
        : base(options)
        {
        }
        public DbSet<GradeItem> Grades => Set<GradeItem>();
    }
}