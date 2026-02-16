namespace AlertEngine.API.Models
{
    public class AlertEvent
    {
        public string Source { get; set; } = default!;
        public string Metric { get; set; } = default!;
        public double Value { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
