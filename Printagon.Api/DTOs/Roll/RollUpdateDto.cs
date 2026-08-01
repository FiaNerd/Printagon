public class RollUpdateDto
{
    public int RollWeight { get; set; }
    public int? RollWeightLeftOver { get; set; }
    public string? Comment { get; set; }

    public string? PaperType { get; set; }
    public int? PaperGramWeight { get; set; }
    public int? PaperWidth { get; set; }

    public Guid? CreatedBy { get; set; }  
}
