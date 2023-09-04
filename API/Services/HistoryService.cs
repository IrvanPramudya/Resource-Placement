using API.Contracts;
using API.DTOs.Histories;
using API.DTOs.NewHistoryDto;
using API.Models;

namespace API.Services
{
    public class HistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRoleRepository _accountroleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPositionRepository _positionRepository;
        public HistoryService(IHistoryRepository historyRepository, IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IAccountRoleRepository accountroleRepository, IInterviewRepository interviewRepository, IClientRepository clientRepository, IPositionRepository positionRepository)
        {
            _historyRepository = historyRepository;
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
            _accountroleRepository = accountroleRepository;
            _interviewRepository = interviewRepository;
            _clientRepository = clientRepository;
            _positionRepository = positionRepository;
        }

        public IEnumerable<HistoryDto> GetAllHistoriesWithName()
        {
            var histories = from employee in _employeeRepository.GetAll()
                            join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
                            join accountrole in _accountroleRepository.GetEmployeewithEmployeeRole() on account.Guid equals accountrole.AccountGuid
                            join history in _historyRepository.GetAll() on employee.Guid equals history.EmployeeGuid
                            join client in _clientRepository.GetAll() on history.ClientGuid equals client.Guid
                            join position in _positionRepository.GetAll() on history.PositionGuid equals position.Guid
                            select new HistoryDto
                            {
                                Guid = history.Guid,
                                PositionGuid = history.PositionGuid,
                                ClientGuid = history.ClientGuid,
                                EmployeeGuid = history.Guid,
                                FullName = employee.FirstName +" " + employee.LastName,
                                ClientName = client.Name,
                                PositionName = position.Name,
                                InterviewDate = history.InterviewDate,
                                IsAccepted = history.IsAccepted,
                                Status = history.Status
                                
                            };
            return histories;
        }
        public IEnumerable<HistoryDto> GetAll()
        {
            var histories = _historyRepository.GetAll();
            if (!histories.Any())
            {
                return Enumerable.Empty<HistoryDto>(); // History is null or not found;
            }

            var historyDtos = new List<HistoryDto>();
            foreach (var history in histories)
            {
                historyDtos.Add((HistoryDto)history);
            }

            return historyDtos; // History is found;
        }

        public HistoryDto? GetByGuid(Guid guid)
        {
            var history = _historyRepository.GetByGuid(guid);
            if (history is null)
            {
                return null; // History is null or not found;
            }

            return (HistoryDto)history; // History is found;
        }

        public HistoryDto? Create(NewHistoryDto newHistoryDto)
        {
            var history = _historyRepository.Create(newHistoryDto);
            if (history is null)
            {
                return null; // History is null or not found;
            }

            return (HistoryDto)history; // History is found;
        }

        public int Update(HistoryDto historyDto)
        {
            var history = _historyRepository.GetByGuid(historyDto.Guid);
            if (history is null)
            {
                return -1; // History is null or not found;
            }

            History toUpdate = historyDto;
            toUpdate.CreatedDate = history.CreatedDate;
            var result = _historyRepository.Update(toUpdate);

            return result ? 1 // History is updated;
                : 0; // History failed to update;
        }

        public int Delete(Guid guid)
        {
            var history = _historyRepository.GetByGuid(guid);
            if (history is null)
            {
                return -1; // History is null or not found;
            }

            var result = _historyRepository.Delete(history);

            return result ? 1 // History is deleted;
                : 0; // History failed to delete;
        }
    }
}
