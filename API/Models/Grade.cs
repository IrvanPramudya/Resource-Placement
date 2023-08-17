using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_grades")]
    public class Grade:BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("salary")]
        public int Salary { get; set; }

        //Cardinality
        public Employee? Employee { get; set; }
    }
}
