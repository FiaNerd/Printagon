namespace Printagon.Api.DTOs
{
    public class OrderRollDto
    {
        public Guid RollId { get; set; }
        public Guid OrderId { get; set; }

        public int UsedWeight { get; set; }

    }
}