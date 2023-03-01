using System.ComponentModel.DataAnnotations;

namespace StudentsGrades.Data
{
    public class GradeItem
    {
        public int GradeItemId { get; set; }

        [Required(ErrorMessage = "Имя не должно быть пустым!")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Название предмета не должно быть пустым!")]
        public string Subject { get; set; } = "";

        [Required(ErrorMessage = "Оценка должна быть выставлена!")]
        [Range(0, 10, ErrorMessage = "Оценка должна быть числом от 0 до 10!")]
        public int Grade { get; set; }
    }
}
