using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_clients")]
    public class Client:BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column("is_available")]
        public bool IsAvailable { get; set; }
        //Cardinality
        /*public Employee? Employee { get; set;}*/
        public ICollection<Interview>? Interviews { get; set;}
        public ICollection<Position>? Positions { get; set;}
        public ICollection<Placement>? Placements { get; set;}

    }
}
