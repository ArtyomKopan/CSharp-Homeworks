using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsGrades.Data;
using System.Threading.Tasks;

namespace StudentsGrades.Pages
{
    [BindProperties]
    public class AddGradeModel : PageModel
    {
        private readonly StudentsGradesDbContext _context;

        /// <summary>
        /// Initialize database context
        /// </summary>
        /// <param name="context"></param>
        public AddGradeModel(StudentsGradesDbContext context) => _context = context;

        public GradeItem GradeItem { get; set; } = new();

        /// <summary>
        /// Execute post request in HTML-form on page AddGrade and 
        /// write grade item to database. 
        /// </summary>
        /// <returns> Redirect on start page </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Grades.Add(GradeItem);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
