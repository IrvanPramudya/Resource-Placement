using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_employees")]
    public class Employee:BaseEntity
    {
        [Column("nik", TypeName = "nchar(6)")]
        public string NIK { get; set; }
        [Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Column("last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        [Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }
        [Column("gender")]
        public GenderLevel Gender { get; set; }
        [Column("status")]
        public StatusLevel Status { get; set; }
        [Column("skill")]
        public string? Skill { get; set; }
        /*[Column("grade_guid")]
        public Guid GradeGuid { get; set; }*/
/*        [Column("client_guid")]
        public Guid ClientGuid { get; set; }*/

        //Cardinality
        public Account? Account { get; set; }
        public Interview? Interview { get; set; }
        public IEnumerable<History>? Histories { get; set; }
        public Grade? Grade { get; set; }
        /*public Client? Client { get; set; }*/
        public Placement? Placement { get; set; }

    }
}
