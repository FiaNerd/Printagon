namespace Printagon.Api.DTOs.OrderRoll
{
    public class OrderRollCreateDto
    {
        public Guid OrderId { get; set; }
        public Guid RollId { get; set; }
        public int NewRollWeight { get; set; }
    }
}
