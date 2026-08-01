using Printagon.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class OrderRoll
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public int OrderNumber { get; set; }

    [JsonIgnore]
    public Order Order { get; set; } = null!;

    public int RollNumber { get; set; }

    [JsonIgnore]
    public Roll Roll { get; set; } = null!;

    public int IntakeWeight { get; set; }
    public int? OutputWeight { get; set; }

    [NotMapped]
    [JsonIgnore]
    public int? ConsumedWeight =>
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
