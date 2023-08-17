using API.Models;

namespace API.DTOs.Positions
{
    public class NewPositionDto
    {
        public Guid ClientGuid { get; set; }
        public string Name { get; set; }

        public static implicit operator Position(NewPositionDto position)
        {
            return new Position
            {
                Guid = new Guid(),
                Name = position.Name,
                ClientGuid = position.ClientGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator NewPositionDto(Position position)
        {
            return new NewPositionDto
            {
                Name = position.Name,
                ClientGuid = position.ClientGuid,
            };
        }

    }
}
