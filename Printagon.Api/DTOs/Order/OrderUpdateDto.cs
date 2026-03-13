using Printagon.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Printagon.Api.DTOs.Order
{
    public class OrderUpdateDto
    {
        [Required]
        public int OrderNumber { get; set; }

        [Required] 
        public string JobName { get; set; } = string.Empty;
        public int? YearlyNumber { get; set; }
        
        [Required] 
        public string PaperType { get; set; } = string.Empty;

        [Required]
        public int GramWeight { get; set; }

        [Required]
        public int RollWidth { get; set; }

        [Required]
        public string OrderStatus { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}
