using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_positions")]
    public class Position:BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("client_guid")]
        public Guid ClientGuid { get; set; }
        //Cardinality
        /*public Interview? Interview { get; set; }*/
        public Client? Client { get; set; }
    }
}
