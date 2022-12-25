using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsGrades.Data;

namespace StudentsGrades.Pages
{
    public class GradesListModel : PageModel
    {
        private readonly StudentsGradesDbContext _context;

        public GradesListModel(StudentsGradesDbContext context) => _context = context;

        public IList<GradeItem> Grades { get; private set; } = new List<GradeItem>();

        public void OnGet()
        {
            Grades = _context.Grades.OrderBy(p => p.GradeItemId).ToList();
        }
    }
}
