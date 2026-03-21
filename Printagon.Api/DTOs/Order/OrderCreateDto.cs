using Printagon.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Printagon.Api.DTOs.Order
{
    public class OrderCreateDto
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
        public OrderStatus OrderStatus { get; set; }
        public string? Comment { get; set; }

    }
}
