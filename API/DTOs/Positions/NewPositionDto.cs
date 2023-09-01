using API.Models;

namespace API.DTOs.Positions
{
    public class NewPositionDto
    {
        /*public Guid Guid { get; set; }*/
        public Guid ClientGuid { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public static implicit operator Position(NewPositionDto position)
        {
            return new Position
            {
                Guid = new Guid(),
                ClientGuid = position.ClientGuid,
                Name = position.Name,
                Capacity = position.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
        public static explicit operator NewPositionDto(Position position)
        {
            return new NewPositionDto
            {
                Name = position.Name,
                Capacity = position.Capacity,
            };
        }

    }
}
