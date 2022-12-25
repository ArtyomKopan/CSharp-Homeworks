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

        public AddGradeModel(StudentsGradesDbContext context) => _context = context;

        public GradeItem GradeItem { get; set; } = new();

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
