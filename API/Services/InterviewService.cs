using API.Contracts;
using API.Models;
using API.Repositories;

namespace API.Services
{
    public class InterviewService
    {
        private readonly IInterviewRepository _interviewRepository;

        public InterviewService(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        public IEnumerable<InterviewDto> GetAll()
        {
            var interviews = _interviewRepository.GetAll();
            if (!interviews.Any())
            {
                return Enumerable.Empty<InterviewDto>(); // Interview is null or not found;
            }

            var interviewDtos = new List<InterviewDto>();
            foreach (var interview in interviews)
            {
                interviewDtos.Add((InterviewDto)interview);
            }

            return interviewDtos; // Interview is found;
        }

        public InterviewDto? GetByGuid(Guid guid)
        {
            var interview = _interviewRepository.GetByGuid(guid);
            if (interview is null)
            {
                return null; // Interview is null or not found;
            }

            return (InterviewDto)interview; // Interview is found;
        }

        public InterviewDto? Create(NewInterviewDto newInterviewDto)
        {
            var interview = _interviewRepository.Create(newInterviewDto);
            if (interview is null)
            {
                return null; // Interview is null or not found;
            }

            return (InterviewDto)interview; // Interview is found;
        }

        public int Update(InterviewDto interviewDto)
        {
            var interview = _interviewRepository.GetByGuid(interviewDto.Guid);
            if (interview is null)
            {
                return -1; // Interview is null or not found;
            }

            Interview toUpdate = interviewDto;
            toUpdate.CreatedDate = interview.CreatedDate;
            var result = _interviewRepository.Update(toUpdate);

            return result ? 1 // Interview is updated;
                : 0; // Interview failed to update;
        }

        public int Delete(Guid guid)
        {
            var interview = _interviewRepository.GetByGuid(guid);
            if (interview is null)
            {
                return -1; // Interview is null or not found;
            }

            var result = _interviewRepository.Delete(interview);

            return result ? 1 // Interview is deleted;
                : 0; // Interview failed to delete;
        }
    }
}
