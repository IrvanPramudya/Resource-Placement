using API.Models;

namespace API.DTOs.Placements
{
    public class NewPlacementDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
<<<<<<< Updated upstream
        public Guid EmployeeGuid { get; set; }
        public Guid ClientGuid { get; set; }
=======
        public Guid Guid { get; set; }
>>>>>>> Stashed changes

        public static implicit operator Placement(NewPlacementDto placement)
        {
            return new Placement
            {
                Guid = new Guid(),
                StartDate = placement.StartDate,
                EndDate = placement.EndDate,
<<<<<<< Updated upstream
                EmployeeGuid = placement.EmployeeGuid,
                ClientGuid = placement.ClientGuid,
=======
                Guid = placement.Guid,
>>>>>>> Stashed changes
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,

            };
        }
        public static explicit operator NewPlacementDto(Placement placement)
        {
            return new NewPlacementDto
            {
                StartDate = placement.StartDate,
                EndDate = placement.EndDate,
<<<<<<< Updated upstream
                EmployeeGuid = placement.EmployeeGuid,
                ClientGuid = placement.ClientGuid,
=======
                Guid = placement.Guid,
>>>>>>> Stashed changes

            };
        }
    }
}
