using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_positions")]
    public class Position:BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("capacity")]
        public int Capacity { get; set; }
        //Cardinality
        /*public Interview? Interview { get; set; }*/
        public Client? Client { get; set; }
    }
}
