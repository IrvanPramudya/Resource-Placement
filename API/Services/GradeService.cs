using API.Contracts;
using API.DTOs.Grades;
using API.Models;

namespace API.Services
{
    public class GradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public IEnumerable<GradeDto> GetAll()
        {
            var grades = _gradeRepository.GetAll();
            if (!grades.Any())
            {
                return Enumerable.Empty<GradeDto>(); // Grade is null or not found;
            }

            var gradeDtos = new List<GradeDto>();
            foreach (var grade in grades)
            {
                gradeDtos.Add((GradeDto)grade);
            }

            return gradeDtos; // Grade is found;
        }

        public GradeDto? GetByGuid(Guid guid)
        {
            var grade = _gradeRepository.GetByGuid(guid);
            if (grade is null)
            {
                return null; // Grade is null or not found;
            }

            return (GradeDto)grade; // Grade is found;
        }

        public GradeDto? Create(NewGradeDto newGradeDto)
        {
            var grade = _gradeRepository.Create(newGradeDto);
            if (grade is null)
            {
                return null; // Grade is null or not found;
            }

            return (GradeDto)grade; // Grade is found;
        }

        public int Update(GradeDto gradeDto)
        {
            var grade = _gradeRepository.GetByGuid(gradeDto.Guid);
            if (grade is null)
            {
                return -1; // Grade is null or not found;
            }

            Grade toUpdate = gradeDto;
            toUpdate.CreatedDate = grade.CreatedDate;
            var result = _gradeRepository.Update(toUpdate);

            return result ? 1 // Grade is updated;
                : 0; // Grade failed to update;
        }

        public int Delete(Guid guid)
        {
            var grade = _gradeRepository.GetByGuid(guid);
            if (grade is null)
            {
                return -1; // Grade is null or not found;
            }

            var result = _gradeRepository.Delete(grade);

            return result ? 1 // Grade is deleted;
                : 0; // Grade failed to delete;
        }
    }
}
