using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.NewHistoryDto
{
    public class NewHistoryDto
    {
        public Guid EmployeeGuid { get; set; }
        public Guid ClientGuid { get; set; }
        public Guid PositionGuid { get; set; }
        public InterviewLevel Status { get; set; }
        public bool? IsAccepted { get; set; }
        public DateTime InterviewDate { get; set; }

        public static implicit operator History(NewHistoryDto history)
        {
            return new History
            {
                Guid = new Guid(),
                EmployeeGuid = history.EmployeeGuid,
                ClientGuid = history.ClientGuid,
                InterviewDate = history.InterviewDate,
                Status = history.Status,
                PositionGuid = history.PositionGuid,
                IsAccepted = history.IsAccepted,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator NewHistoryDto(History history)
        {
            return new NewHistoryDto
            {
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
