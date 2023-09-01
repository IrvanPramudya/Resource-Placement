using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_placement")]
    public class Placement:BaseEntity
    {
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        /*[Column("employee_guid")]
        public Guid EmployeeGuid { get; set; }*/
        [Column("client_guid")]
        public Guid ClientGuid { get; set; }
        [Column("position_guid")]
        public Guid PositionGuid { get; set; }

        //Cardinality
        public Employee? Employee { get; set; }
        public Client? Client { get; set; }
    }
}
