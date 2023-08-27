using API.Contracts;
using API.Data;
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
        private readonly PlacementDbContext _dbContext;

        public InterviewService(IInterviewRepository interviewRepository, IEmployeeRepository employeeRepository, IClientRepository clientRepository, IPositionRepository positionRepository, IEmailHandler emailHandler, IPlacementRepository placementRepository, IGradeRepository gradeRepository, PlacementDbContext dbContext)
        {
            _interviewRepository = interviewRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _positionRepository = positionRepository;
            _emailHandler = emailHandler;
            _placementRepository = placementRepository;
            _gradeRepository = gradeRepository;
            _dbContext = dbContext;
        }
        public IEnumerable<GetRemainingEmployee> GetAllInterviewEmployeePlacement()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join placement in _placementRepository.GetAll() on employee.Guid equals placement.Guid into placementgroup
                        from placement in placementgroup.DefaultIfEmpty()
                        join interview in _interviewRepository.GetAll() on employee.Guid equals interview.Guid into interviewgroup
                        from interview in interviewgroup.DefaultIfEmpty()
                        select new GetRemainingEmployee
                        {
                            Guid = employee.Guid,
                            Status = interview != null? interview.Status : null,
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
            return merge;
        }
        public IEnumerable<GetRemainingEmployee> GetEmployeeOuterJoinInterview()
        {
             return GetAllInterviewEmployeePlacement().Where(inter=>inter.InterviewDate == null && inter.PlacementGuid == null);
        }
        public IEnumerable<GetRemainingEmployee> GetEmployeeOuterJoinPlacement()
        {
             return GetAllInterviewEmployeePlacement().Where(inter=> inter.PlacementGuid == null && inter.Status == Utilities.Enums.InterviewLevel.AcceptedbyClient);
        }
        public int UpdateFullInterview(InterviewDto interviewDto)
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
        public IEnumerable<InformatifInterview> GetUnresultInterviews()
        {
            return GetInterviews().Where(interview => interview.IsAccepted==false 
            && interview.Status == Utilities.Enums.InterviewLevel.AcceptedbyEmployee);
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
                            IsAccepted = interview.IsAccepted,
                            Status = interview.Status,
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
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var grade = _gradeRepository.GetByGuid(newInterviewDto.Guid);
                if (grade == null)
                {
                    return null;
                }
                _gradeRepository.Clear();
                var placement = _placementRepository.GetByGuid(newInterviewDto.Guid);
                if (placement != null)
                {
                    return null;
                }
                _placementRepository.Clear();
                Interview interviewtoCreate = newInterviewDto;
                interviewtoCreate.IsAccepted = false;
                interviewtoCreate.Status = Utilities.Enums.InterviewLevel.EmployeeResponWaiting;
                var interview = _interviewRepository.Create(interviewtoCreate);
                if (interview is null)
                {
                    return null; // Interview is null or not found;
                }
                _interviewRepository.Clear();
                var position = _positionRepository.GetByClientGuid(newInterviewDto.ClientGuid); //Revisi Menggunakan GetByClient
                _positionRepository.Clear();
                var positionUpdate = _positionRepository.Update(new Position
                {
                    Guid = position.Guid,
                    ClientGuid = newInterviewDto.ClientGuid,
                    Name = position.Name,
                    Capacity = position.Capacity - 1,
                    CreatedDate = position.CreatedDate,
                    ModifiedDate = DateTime.Now

                });
                _positionRepository.Clear();
                var employee = _employeeRepository.GetByGuid(interview.Guid);
                var client = _clientRepository.GetByGuid(interview.ClientGuid);
                var formattedDate = interview.InterviewDate.ToString("dddd, dd/MM/yy HH:mm");
                var gender = employee.Gender == 0 ? "Female" : "Male";

                _emailHandler.SendEmail(employee.Email, $"Interview Schedule with {client.Name}",
                    $"Congratulations you've been given chance to get interview with {client.Name} on {formattedDate} " +
                    $"and this is the remarks that our company give : {interview.Text}.<br /> Please be Prepared and Keep up the Spirit");
                _emailHandler.SendEmail(client.Email, $"Interview Schedule with {employee.FirstName} {employee.LastName}",
                    $"For the honours of our Company we want you to check Interview Schedule that arranged earlier this is " +
                    $"few data of the schedule<br /> " +
                    $"<table style='border:1px'>" +
                        $"<tr>" +
                            $"<th>Employee Name</th>    " +
                            $"<th>Gender</th>    " +
                            $"<th>Skill</th>    " +
                            $"<th>Interview Date</th>    " +
                            $"<th>Note</th>" +
                        $"</tr>" +
                        $"<tr>    " +
                            $"<td>{employee.FirstName} {employee.LastName}</td>    " +
                            $"<td>{gender}</td>    " +
                            $"<td>{employee.Skill}</td>    " +
                            $"<td>{formattedDate}</td>    " +
                            $"<td>{interview.Text}</td>" +
                        $"</tr>" +
                    $"</table>"+
                    $"<br /> Thank your for the attention hope we will get better at our collaboration");
                transaction.Commit();
                return (InterviewDto)interview; // Interview is found;
            }
            catch
            {
                transaction.Rollback();
                return null;
            }
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
