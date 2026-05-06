using Printagon.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderRoll
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public Guid RollId { get; set; }
    public Roll Roll { get; set; } = null!;
    public decimal IntakeWeight { get; set; }
    public decimal? OutputWeight { get; set; }

    [NotMapped]
    public decimal? ConsumedWeight =>
        OutputWeight.HasValue ? IntakeWeight - OutputWeight.Value : null;

    public string? PaperType { get; set; }
    public int? PaperGramWeight { get; set; }
    public int? PaperWidth { get; set; }
    public bool MatchesOrderPaper { get; set; }
    public string? DeviationReason { get; set; }

    public int? WebBreakCount { get; set; }
    public bool IsRestRoll { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}