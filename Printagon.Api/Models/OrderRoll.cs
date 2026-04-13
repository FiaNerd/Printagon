using Printagon.Api.Models;

public class OrderRoll
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public Guid RollId { get; set; }
    public Roll Roll { get; set; } = null!;

    public int IntakeWeight { get; set; }
    public int? OutputWeight { get; set; }
    public int? ConsumedWeight { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}