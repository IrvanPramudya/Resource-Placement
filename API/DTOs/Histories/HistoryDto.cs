using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Histories
{
    public class HistoryDto
    {
        public Guid Guid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public Guid ClientGuid { get; set; }
        public Guid PositionGuid { get; set; }
        public InterviewLevel Status { get; set; }
        public bool? IsAccepted { get; set; }
        public DateTime InterviewDate { get; set; }
        public string FullName { get; set; }
        public string ClientName { get; set; }
        public string PositionName { get; set; }
        public string Email { get; set; }

        public static implicit operator History(HistoryDto history)
        {
            return new History
            {
                Guid = history.Guid,
                EmployeeGuid = history.EmployeeGuid,
                ClientGuid = history.ClientGuid,
                InterviewDate = history.InterviewDate,
                Status = history.Status,
                PositionGuid = history.PositionGuid,
                IsAccepted = history.IsAccepted,
                ModifiedDate =  DateTime.Now,
            };
        }
        public static explicit operator HistoryDto(History history)
        {
            return new HistoryDto
            {
                Guid = history.Guid,
                EmployeeGuid = history.EmployeeGuid,
                ClientGuid = history.ClientGuid,
                InterviewDate = history.InterviewDate,
                Status = history.Status,
                PositionGuid = history.PositionGuid,
                IsAccepted = history.IsAccepted,
            };
        }
    }
}
