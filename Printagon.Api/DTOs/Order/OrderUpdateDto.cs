namespace Printagon.Api.DTOs.Order
{
    public class OrderUpdateDto
    {
        public int OrderNumber { get; set; }
        public string JobName { get; set; } = string.Empty;
        public int? YearlyNumber { get; set; }
        public string PaperType { get; set; } = string.Empty;
        public int GramWeight { get; set; }
        public int RollWidth { get; set; }
        public Enums.OrderStatus OrderStatus { get; set; }
        public string? Comment { get; set; }
    }
}
