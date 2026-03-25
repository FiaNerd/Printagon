using System.ComponentModel.DataAnnotations;

namespace Printagon.Api.DTOs.Roll
{
    public class RollCreateDto
    {
        [Required]
        public int RollNumber { get; set; }

        [Required]
        public int RollWeight { get; set; }

        public int? RollWeightLeftOver { get; set; }
        public string? Comment { get; set; }

        // Optional overrides from order defaults
        public string? PaperTypeOverride { get; set; }
        public int? PaperGramWeightOverride { get; set; }
        public int? RollWidthOverride { get; set; }
    }
}
