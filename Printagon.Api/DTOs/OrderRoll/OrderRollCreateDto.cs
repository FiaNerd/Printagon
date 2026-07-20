namespace Printagon.Api.DTOs.OrderRoll
{
    public class OrderRollCreateDto
    {
        public Guid OrderId { get; set; }
        public Guid RollId { get; set; }

        public int IntakeWeight { get; set; }

        public string? PaperType { get; set; }
        public int? PaperGramWeight { get; set; }
        public int? PaperWidth { get; set; }

        public bool IsRestRoll { get; set; }

        public string? DeviationReason { get; set; }
        public int? WebBreakCount { get; set; }
    }
}
