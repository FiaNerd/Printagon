namespace Printagon.Api.DTOs.OrderRoll
{
    public class OrderRollResponseDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
            public Guid RollId { get; set; }

            public int IntakeWeight { get; set; }
            public int? OutputWeight { get; set; }
            public int? ConsumedWeight { get; set; }

            public int? RollNumber { get; set; }
            public string? PaperType { get; set; }
            public int? PaperGramWeight { get; set; }
            public int? PaperWidth { get; set; }
       
    }
}