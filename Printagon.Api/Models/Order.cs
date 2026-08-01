using Printagon.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Printagon.Api.Models
{
    public class Order
    {
            [Key]
            public Guid Id { get; set; } = Guid.NewGuid();
            public int OrderNumber { get; set; }
            public string JobName { get; set; } = string.Empty;
            public int? YearlyNumber { get; set; }
            public string PaperType { get; set; } = string.Empty;
            public int PaperGramWeight { get; set; }
            public int PaperWidth { get; set; }
            public OrderStatus OrderStatus { get; set; }
            public string? Comment { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
            public ICollection<OrderRoll> OrderRolls { get; set; } = new List<OrderRoll>();
        }
    
}