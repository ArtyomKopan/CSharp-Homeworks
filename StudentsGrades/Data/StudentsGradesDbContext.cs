using Microsoft.EntityFrameworkCore;

namespace StudentsGrades.Data
{
    public class StudentsGradesDbContext : DbContext
    {
        /// <summary>
        /// Added options to database context
        /// </summary>
        /// <param name="options"></param>
        public StudentsGradesDbContext(
        DbContextOptions<StudentsGradesDbContext> options)
        : base(options)
        {
        }

        /// <summary>
        /// Create new table with students grades in database
        /// </summary>
        public DbSet<GradeItem> Grades => Set<GradeItem>();
    }
}