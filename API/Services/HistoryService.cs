using API.Contracts;
using API.DTOs.Histories;
using API.DTOs.NewHistoryDto;
using API.Models;

namespace API.Services
{
    public class HistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
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
