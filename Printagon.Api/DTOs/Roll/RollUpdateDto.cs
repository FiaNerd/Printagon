using System.ComponentModel.DataAnnotations;

namespace Printagon.Api.DTOs.Roll
{
    public class RollUpdateDto
    {
        [Required]
        public int RollNumber { get; set; }

        public string? PaperType { get; set; }
        public int? PaperWidth { get; set; }
        public int? PaperGramWeight { get; set; }


        [Required]
        public int RollWeight { get; set; }

        public int? RollWeightLeftOver { get; set; }
        public string? Comment { get; set; }
    }
}
