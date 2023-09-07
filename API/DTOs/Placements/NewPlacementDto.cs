using API.Models;

namespace API.DTOs.Placements
{
    public class NewPlacementDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid Guid { get; set; }

        public static implicit operator Placement(NewPlacementDto placement)
        {
            return new Placement
            {
                StartDate = placement.StartDate,
                EndDate = placement.EndDate,
                Guid = placement.Guid,
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
                Guid = placement.Guid,

            };
        }
    }
}
