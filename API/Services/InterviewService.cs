using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Interviews;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using static System.Net.WebRequestMethods;

namespace API.Services
{
    public class InterviewService
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IPlacementRepository _placementRepository;
        private readonly IEmailHandler _emailHandler;

        public InterviewService(IInterviewRepository interviewRepository, IEmployeeRepository employeeRepository, IClientRepository clientRepository, IPositionRepository positionRepository, IEmailHandler emailHandler, IPlacementRepository placementRepository, IGradeRepository gradeRepository)
        {
            _interviewRepository = interviewRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _positionRepository = positionRepository;
            _emailHandler = emailHandler;
            _placementRepository = placementRepository;
            _gradeRepository = gradeRepository;
        }
        public IEnumerable<GetRemainingEmployee> GetEmployeeOuterJoinInterview()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join placement in _placementRepository.GetAll() on employee.Guid equals placement.Guid into placementgroup
                        from placement in placementgroup.DefaultIfEmpty()
                        join interview in _interviewRepository.GetAll() on employee.Guid equals interview.Guid into interviewgroup
                        from interview in interviewgroup.DefaultIfEmpty()
                        select new GetRemainingEmployee
                        {
                            Guid = employee.Guid,
                            ClientGuid = interview != null? interview.ClientGuid:null,
                            InterviewDate = interview !=null? interview.InterviewDate:null,
                            FullName = employee.FirstName + " " + employee.LastName,
                            PlacementGuid = placement != null? placement.Guid : null,
                            StartDate = placement != null? placement.StartDate:null,
                        };
            if(!merge.Any())
            {
                return null;
            }
            return merge.Where(inter=>inter.InterviewDate == null && inter.StartDate == null);
        }
        public IEnumerable<InformatifInterview> GetInterviews()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join interview in _interviewRepository.GetAll() on employee.Guid equals interview.Guid
                        join client in _clientRepository.GetAll() on interview.ClientGuid equals client.Guid
                        select new InformatifInterview
                        {
                            ClientGuid = interview.ClientGuid,
                            Guid = employee.Guid,
                            ClientName = client.Name,
                            FullName = employee.FirstName+ " "+ employee.LastName,
                            InterviewDate = interview.InterviewDate,
                            Text = interview.Text
                        };
            return merge;
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
            var grade =_gradeRepository.GetByGuid(newInterviewDto.Guid);
            if(grade is null)
            {
                return null;
            }
            var placement = _placementRepository.GetByGuid(newInterviewDto.Guid);
            if(!(placement is null))
            {
                return null;
            }
            var interview = _interviewRepository.Create(new InterviewDto
            {
                ClientGuid = newInterviewDto.ClientGuid,
                Guid = newInterviewDto.Guid,
                InterviewDate = newInterviewDto.InterviewDate,
                Text = newInterviewDto.Text,
                IsAccepted = false,
            });
            if (interview is null)
            {
                return null; // Interview is null or not found;
            }
            var position = _positionRepository.GetByGuid(newInterviewDto.ClientGuid);
            var positionUpdate = _positionRepository.Update(new Position
            {
                Guid = newInterviewDto.ClientGuid,
                Name = position.Name,
                Capacity = position.Capacity - 1,
                CreatedDate= position.CreatedDate,
                ModifiedDate = DateTime.Now

            });
            var employee = _employeeRepository.GetByGuid(interview.Guid);
            var client = _clientRepository.GetByGuid(interview.ClientGuid);
            var formattedDate = interview.InterviewDate.ToString("dddd, dd/MM/yy HH:mm");
            var gender = employee.Gender == 0 ? "Female" : "Male";

            _emailHandler.SendEmail(employee.Email, $"Interview Schedule with {client.Name}", 
                $"Congratulations you've been given chance to get interview with {client.Name} on {formattedDate} " +
                $"and this is the remarks that our company give : {interview.Text}.<br /> Please be Prepared and Keep up the Spirit");
            _emailHandler.SendEmail(client.Email, $"Interview Schedule with {employee.FirstName} {employee.LastName}", 
                $"For the honours of our Company we want you to check Interview Schedule that arranged earlier this is " +
                $"few data of the schedule<br /> Employee Name : {employee.FirstName} {employee.LastName}<br /> Gender : {gender}<br /> " +
                $"Skill : {employee.Skill}<br /> Interview Date : {formattedDate}<br /> Note : {interview.Text}" +
                $"<br /> Thank your for the attention hope we will get better at our collaboration");
            return (InterviewDto)interview; // Interview is found;
        }

        public int Update(NewInterviewDto interviewDto)
        {
            var interview = _interviewRepository.GetByGuid(interviewDto.Guid);
            if (interview is null)
            {
                return -1; // Interview is null or not found;
            }

            Interview toUpdate = interviewDto;
            toUpdate.IsAccepted = interview.IsAccepted;
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
