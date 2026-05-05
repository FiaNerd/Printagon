namespace Printagon.Api.DTOs.OrderRoll
{
    public class OrderRollPatchDto
    {
        public int? OutputWeight { get; set; }
        public bool? MatchesOrderPaper { get; set; }
        public string? DeviationReason { get; set; }
        public bool? IsRestRoll { get; set; }
        public int? WebBreak { get; set; }
    }
}
