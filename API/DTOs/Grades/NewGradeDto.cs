using API.Models;

namespace API.DTOs.Grades
{
    public class NewGradeDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }

        public static implicit operator Grade(NewGradeDto grade)
        {
            return new Grade
            {
                Guid = grade.Guid,
                Name = grade.Name,
                Salary = grade.Salary,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator NewGradeDto(Grade grade)
        {
            return new NewGradeDto
            {
                Guid = grade.Guid,
                Name = grade.Name,
                Salary = grade.Salary,
            };
        }
    }
}
