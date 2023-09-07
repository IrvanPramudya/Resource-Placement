using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Histories;
using API.DTOs.Interviews;
using API.DTOs.NewHistoryDto;
using API.DTOs.Placements;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.OpenApi.Extensions;
using static System.Net.WebRequestMethods;

namespace API.Services
{
    public class InterviewService
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountroleRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IPlacementRepository _placementRepository;
        private readonly IHistoryRepository _historyRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly PlacementDbContext _dbContext;

        public InterviewService(IInterviewRepository interviewRepository, IEmployeeRepository employeeRepository, IClientRepository clientRepository, IPositionRepository positionRepository, IEmailHandler emailHandler, IPlacementRepository placementRepository, IGradeRepository gradeRepository, PlacementDbContext dbContext, IAccountRepository accountRepository, IAccountRoleRepository accountroleRepository, IHistoryRepository historyRepository)
        {
            _interviewRepository = interviewRepository;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _positionRepository = positionRepository;
            _emailHandler = emailHandler;
            _placementRepository = placementRepository;
            _gradeRepository = gradeRepository;
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _accountroleRepository = accountroleRepository;
            _historyRepository = historyRepository;
        }
        public IEnumerable<GetCountedClient> GetCountedInterview()
        {
            var interview = _interviewRepository.GetAll();
            var client = _clientRepository.GetAll();
            var listclient = new List<GetCountedClient>();
            foreach (var item in client)
            {
                var newclient = new GetCountedClient
                {
                    ClientName = item.Name,
                    CountEmployee = 0
                };
                foreach (var item2 in interview)
                {
                    if (item.Guid == item2.ClientGuid)
                    {
                        newclient.CountEmployee++;
                    }
                }
                listclient.Add(newclient);
            }
            return listclient;
        }
        public IEnumerable<GetRemainingEmployee> GetAllInterviewEmployeePlacement()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                        join accountrole in _accountroleRepository.GetEmployeewithEmployeeRole() on account.Guid equals accountrole.AccountGuid
                        join placement in _placementRepository.GetAll() on employee.Guid equals placement.Guid into placementgroup
                        from placement in placementgroup.DefaultIfEmpty()
                        join interview in _interviewRepository.GetAll() on employee.Guid equals interview.Guid
                        join client in _clientRepository.GetAll() on (interview != null ? interview.ClientGuid : placement.ClientGuid) equals client.Guid into CliGrp
                        from client in CliGrp.DefaultIfEmpty()
                        join position in _positionRepository.GetAll() on client.Guid equals position.ClientGuid
                        where interview.PositionGuid == position.Guid 
                        select new GetRemainingEmployee
                        {
                            Guid = employee.Guid,
                            Status = interview != null? interview.Status : null,
                            ClientName = interview != null? client.Name :null,
                            ClientGuid = interview != null? interview.ClientGuid:null,
                            PositionGuid = interview != null? interview.PositionGuid:null,
                            InterviewDate = interview !=null? interview.InterviewDate:null,
                            FullName = employee.FirstName + " " + employee.LastName,
                            PlacementGuid = placement != null? placement.Guid : null,
                            StartDate = placement != null? placement.StartDate:null,
                            PositionName = position.Name,

                        };
            if(!merge.Any())
            {
                return null;
            }
            return merge;
        }
        public IEnumerable<GetRemainingEmployee> GetAllEmployee()
        {
            var merge = from employee in _employeeRepository.GetAll()
                        join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                        join accountrole in _accountroleRepository.GetEmployeewithEmployeeRole() on account.Guid equals accountrole.AccountGuid
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
        public GetCountedInterviewStatus CountInterviewStatus()
        {
            var interview = _interviewRepository.GetAll();
            var count0 = 0;
            var count1 = 0;
            var count2 = 0;
            var count3 = 0;
            var count4 = 0;
            foreach (var item in interview)
            {
                if(item.Status == InterviewLevel.EmployeeResponWaiting)
                {
                    count0++;
                }
                if(item.Status == InterviewLevel.AcceptedbyEmployee)
                {
                    count1++;
                }
                if(item.Status == InterviewLevel.RejectedbyEmployee)
                {
                    count2++;
                }
                if(item.Status == InterviewLevel.AcceptedbyClient)
                {
                    count3++;
                }
                if(item.Status == InterviewLevel.RejectedbyClient)
                {
                    count4++;
                }
            }

            return new GetCountedInterviewStatus
            {
                CountStatusWaiting = count0,
                CountStatusAcceptedbyEmployee = count1,
                CountStatusRejectedbyEmployee = count2,
                CountStatusAcceptedbyClient = count3,
                CountStatusRejectedbyClient = count4,
            };
        }
        public IEnumerable<GetRemainingEmployee> GetEmployeeOuterJoinInterview()
        {
             return GetAllEmployee().Where(inter=>inter.InterviewDate == null && inter.PlacementGuid == null);
        }
        public IEnumerable<GetRemainingEmployee> GetEmployeeOuterJoinPlacement()
        {
             return GetAllEmployee().Where(inter=> inter.PlacementGuid == null && inter.Status == Utilities.Enums.InterviewLevel.AcceptedbyClient);
        }
        public int UpdateFullInterview(UpdateInterviewDto interviewDto)
        {
            var interview = _interviewRepository.GetByGuid(interviewDto.Guid);
            var history = _historyRepository.GetHistoryByEmployeeGuid(interviewDto.Guid);
            var historyByGuid = history.Where(his => his.InterviewDate.Equals(interview.InterviewDate)).FirstOrDefault();
            _historyRepository.Clear();
            if (interview is null && history is null)
            {
                return -1; // Interview is null or not found;
            }
            Interview toUpdate = interviewDto;
            toUpdate.CreatedDate = interview.CreatedDate;
            toUpdate.InterviewDate = interview.InterviewDate;
            toUpdate.PositionGuid = interview.PositionGuid;
            toUpdate.ClientGuid = interview.ClientGuid;
            toUpdate.Text = interview.Text;
            toUpdate.Comment = interviewDto.Comment;
            var result = _interviewRepository.Update(toUpdate);
            History historyUpdate = historyByGuid;
            historyUpdate.IsAccepted = toUpdate.IsAccepted;
            historyUpdate.Status = toUpdate.Status;
            historyUpdate.CreatedDate = historyByGuid.CreatedDate;
            var UpdateHistory = _historyRepository.Update(historyUpdate);
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
                        join position in _positionRepository.GetAll() on client.Guid equals position.ClientGuid
                        where interview.PositionGuid == position.Guid
                        select new InformatifInterview
                        {
                            ClientGuid = interview.ClientGuid,
                            PositionGuid = interview.PositionGuid,
                            Guid = employee.Guid,
                            PositionName = position.Name,
                            ClientName = client.Name,
                            FullName = employee.FirstName+ " "+ employee.LastName,
                            InterviewDate = interview.InterviewDate,
                            IsAccepted = interview.IsAccepted,
                            Status = interview.Status,
                            Text = interview.Text,
                            Comment = interview.Comment
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
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                Interview interviewtoCreate = newInterviewDto;
                interviewtoCreate.Comment = null;
                interviewtoCreate.IsAccepted = false;
                interviewtoCreate.Status = Utilities.Enums.InterviewLevel.EmployeeResponWaiting;
                var interview = _interviewRepository.Create(interviewtoCreate);
                if (interview is null)
                {
                    return null; // Interview is null or not found;
                }
                _interviewRepository.Clear();
                var getposition = _positionRepository.GetByClientGuid(newInterviewDto.ClientGuid);
                var position = getposition.Where(interview => interview.Guid.Equals(newInterviewDto.PositionGuid)).FirstOrDefault(); //Revisi Menggunakan GetByClient
                /*var selectedPosition = _positionRepository.GetByGuid*/
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
                var history = _historyRepository.Create(new NewHistoryDto
                {
                    EmployeeGuid = newInterviewDto.Guid,
                    ClientGuid= newInterviewDto.ClientGuid,
                    PositionGuid = newInterviewDto.PositionGuid,
                    InterviewDate = newInterviewDto.InterviewDate,
                    IsAccepted = false,
                    Status = InterviewLevel.EmployeeResponWaiting,
                });
                var employee = _employeeRepository.GetByGuid(interview.Guid);
                var client = _clientRepository.GetByGuid(interview.ClientGuid);
                var formattedDate = interview.InterviewDate.ToString("dddd, dd/MM/yy HH:mm");
                var gender = employee.Gender == 0 ? "Female" : "Male";

                _emailHandler.SendEmail(employee.Email, 
                    $"Interview Schedule with {client.Name}",
                    $"<div class='card' style='width: 100%;'>" +
                        $"<div class='card-body'>" +
                            $"<h5 class='card-title'>Congratulations you've been given chance to get interview</h5>" +
                                $"<table class='table table-bordered table-dark'>" +
                                    $"<thead><tr><th>Client Name</th><th>Schedule</th><th>Note</th></tr></thead>" +
                                    $"<tbody><tr><td>{client.Name}</td><td>{formattedDate}</td><td>{interview.Text}</td></tr></tbody>" +
                                $"</table>" +
                            $"<p class='card-text'>Please be Prepared and Keep up the Spirit</p>" +
                        $"</div>" +
                    $"</div>");
                _emailHandler.SendEmail(client.Email, 
                    $"Interview Schedule with {employee.FirstName} {employee.LastName}",
                    $"<div class='card' style='width: 100%;'> " +
                        $"<div class='card-body'> " +
                            $"<h5 class='card-title'>For the honours of our Company we want you to check Interview Schedule that arranged earlier this is few data of the schedule</h5>" +
                            $"<table class='table table-bordered table-dark'>" +
                                $"<thead>" +
                                    $"<tr>" +
                                        $"<th>Employee Name</th>" +
                                        $"<th>Gender</th>" +
                                        $"<th>Skill</th>" +
                                        $"<th>Interview Date</th>" +
                                        $"<th>Note</th>" +
                                    $"</tr>" +
                                $"</thead>" +
                                $"<tbody>" +
                                    $"<tr>" +
                                        $"<td>{employee.FirstName} {employee.LastName}</td>" +
                                        $"<td>{gender}</td>" +
                                        $"<td>{employee.Skill}</td>" +
                                        $"<td>{formattedDate}</td>" +
                                        $"<td>{interview.Text}</td>" +
                                    $"</tr>" +
                                $"</tbody>" +
                            $"</table>" +
                            $"<p class='card-text'>Thank your for the attention hope we will get better at our collaboration</p>" +
                        $"</div>"+
                    $"</div>");
                transaction.Commit();
                return (InterviewDto)interview; // Interview is found;
            }
            catch
            {
                transaction.Rollback();
                return null;
>>>>>>> Stashed changes
            }

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
