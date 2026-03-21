using System.Text.Json.Serialization;

namespace Printagon.Api.Models
{
    public class Roll
    {
        public  Guid Id { get; set; }
        public string? PaperType { get; set; }
        public int? GramWeight { get; set; }
        public int? RollWidth { get; set; }
        public int RollNumber { get; set; }
        public int RollWeight { get; set; }
        public string? Comment { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
