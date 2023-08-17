using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_interview")]
    public class Interview:BaseEntity
    {
        [Column("interview_date")]
        public DateTime InterviewDate { get; set; }
        [Column("text")]
        public string? Text { get; set; }
        [Column("client_guid")]
        public Guid ClientGuid { get; set; }
        /*[Column("position_guid")]
        public Guid PositionGuid { get; set; }*/

        //Cardinality
        public Employee? Employee { get; set; }
        public Client? Client { get; set; }
        /*public Position? Position { get; set; }*/
    }
}
