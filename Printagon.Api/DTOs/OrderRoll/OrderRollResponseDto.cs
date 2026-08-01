namespace Printagon.Api.DTOs.OrderRoll
{
    public class OrderRollResponseDto
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public int RollNumber { get; set; }

        public int IntakeWeight { get; set; }
        public int? OutputWeight { get; set; }
        public int? ConsumedWeight { get; set; }

        public string? PaperType { get; set; }
        public int? PaperGramWeight { get; set; }
        public int? PaperWidth { get; set; }

        public bool MatchesOrderPaper { get; set; }
        public string? DeviationReason { get; set; }

        public int? WebBreakCount { get; set; }
        public bool IsRestRoll { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
