using API.Models;

namespace API.DTOs.Grades
{
    public class GradeDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }

        public static implicit operator Grade(GradeDto grade)
        {
            return new Grade
            {
                Guid = grade.Guid,
                Name = grade.Name,
                Salary = grade.Salary,
                ModifiedDate =  DateTime.Now,
            };
        }
        public static explicit operator GradeDto(Grade grade)
        {
            return new GradeDto
            {
                Guid = grade.Guid,
                Name = grade.Name,
                Salary = grade.Salary,
            };
        }
    }
}
