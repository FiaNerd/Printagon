public class RollCreateDto
{
    public int RollNumber { get; set; }
    public int RollWeight { get; set; }
    public int? RollWeightLeftOver { get; set; }
    public string? Comment { get; set; }

    public string? PaperTypeOverride { get; set; }
    public int? PaperGramWeightOverride { get; set; }
    public int? RollWidthOverride { get; set; }

    public Guid? CreatedBy { get; set; } 
}
