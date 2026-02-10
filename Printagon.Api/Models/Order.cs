using Printagon.Api.Enums;

namespace Printagon.Api.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string JobName { get; set; } = string.Empty;
        public int? YearlyNumber { get; set; }
        public string PaperType { get; set; } = string.Empty;
        public int GramWeight { get; set; }
        public int RollWidth { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
