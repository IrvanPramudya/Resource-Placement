using API.Models;

namespace API.DTOs.Placements
{
    public class PlacementDto
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid EmployeeGuid { get; set; }
        public Guid ClientGuid { get; set; }
        public Guid PositionGuid { get; set; }

        public static implicit operator Placement(PlacementDto placement)
        {
            return new Placement
            {
                Guid = placement.Guid,
                StartDate = placement.StartDate,
                EndDate = placement.EndDate,
                EmployeeGuid = placement.EmployeeGuid,
                ClientGuid = placement.ClientGuid,
                PositionGuid = placement.PositionGuid,
                ModifiedDate = DateTime.Now,

            };
        }
        public static explicit operator PlacementDto(Placement placement)
        {
            return new PlacementDto
            {
                Guid = placement.Guid,
                StartDate = placement.StartDate,
                EndDate = placement.EndDate,
                EmployeeGuid = placement.EmployeeGuid,
                ClientGuid = placement.ClientGuid,
                PositionGuid = placement.PositionGuid,

            };
        }
    }
}
