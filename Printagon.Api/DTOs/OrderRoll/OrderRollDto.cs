namespace Printagon.Api.DTOs.OrderRoll
{
    public class OrderRollDto
    {
        public Guid OrderId { get; set; }
        public Guid RollId { get; set; }
        public int IntakeWeight { get; set; }
    }
}