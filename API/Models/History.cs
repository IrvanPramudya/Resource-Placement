using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_histories")]
    public class History:BaseEntity
    {
        [Column("employee_guid")]
        public Guid EmployeeGuid { get; set; }
        [Column("client_guid")]
        public Guid ClientGuid { get; set; }
        [Column("position_guid")]
        public Guid PositionGuid { get; set; }
        [Column("interview_date")]
        public DateTime InterviewDate { get; set; }
        [Column("status")]
        public InterviewLevel Status { get; set; }
        [Column("is_accepted")]
        public bool? IsAccepted { get; set; }

        //Cardinality
        public Employee? Employee { get; set; }
    }
}
