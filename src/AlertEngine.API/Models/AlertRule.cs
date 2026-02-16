namespace AlertEngine.API.Models
{
    public class AlertRule
    {
        public string RuleId { get; set; } = default!;
        public string Metric { get; set; } = default!;
        public string Operator { get; set; } = default!; // >, <, >=, <=, ==
        public double Threshold { get; set; }
        public string Severity { get; set; } = "Medium";
    }
}
