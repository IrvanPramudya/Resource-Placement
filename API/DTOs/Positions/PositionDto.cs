using API.Models;

namespace API.DTOs.Positions
{
    public class PositionDto
    {
        public Guid Guid { get; set; }
        public Guid ClientGuid { get; set; }
        public string Name { get; set; }

        public static implicit operator Position(PositionDto position)
        {
            return new Position
            {
                Guid = position.Guid,
                Name = position.Name,
                ClientGuid = position.ClientGuid,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator PositionDto(Position position)
        {
            return new PositionDto
            {
                Guid = position.Guid,
                Name = position.Name,
                ClientGuid = position.ClientGuid,
            };
        }
    }
}
