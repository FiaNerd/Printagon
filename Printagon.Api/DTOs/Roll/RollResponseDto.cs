namespace Printagon.Api.DTOs.Roll
{
    public class RollResponseDto
    {
        public Guid Id { get; set; }
        public int RollNumber { get; set; }
        public string? PaperType { get; set; }
        public int? PaperWidth { get; set; }
        public int? PaperGramWeight { get; set; }
        public int RollWeight { get; set; }
        public int? RollWeightLeftOver { get; set; }
        public string? Comment { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
