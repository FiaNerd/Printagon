namespace Printagon.Api.Models
{
    public class Roll
    {
        public  Guid Id { get; set; }
        public string? PaperType { get; set; }
        public int RollNumber { get; set; }
        public int RollWeight { get; set; }
        public string? Comment { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
