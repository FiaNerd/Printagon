namespace Printagon.Api.Models
{
    public class OrderRoll
    {
        public Guid OrderId { get; set; }
        public Guid RollId { get; set; }

        public int UsedWeight { get; set; }

        public Order? Order { get; set; } = null!;
        public Roll? Roll { get; set; } = null!;
    }
}
