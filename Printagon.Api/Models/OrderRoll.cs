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

    public int? ConsumedWeight
        => OutputWeight.HasValue ? IntakeWeight - OutputWeight.Value : null;

    public bool MatchesOrderPaper { get; set; } = true;
    public string? DeviationReason { get; set; }
    public bool IsRestRoll { get; set; } = false;
    public int? WebBreak { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}