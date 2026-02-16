using AlertEngine.API.Models;

namespace AlertEngine.API.Services
{
    public class RuleEvaluator
    {
        private readonly List<AlertRule> _rules;

        public RuleEvaluator(List<AlertRule> rules)
        {
            _rules = rules;
        }

        public IEnumerable<AlertRule> Evaluate(AlertEvent alertEvent)
        {
            return _rules
                .Where(r => r.Metric == alertEvent.Metric)
                .Where(r => EvaluateCondition(r, alertEvent.Value))
                .ToList();
        }

        private bool EvaluateCondition(AlertRule rule, double value)
        {
            return rule.Operator switch
            {
                ">" => value > rule.Threshold,
                "<" => value < rule.Threshold,
                ">=" => value >= rule.Threshold,
                "<=" => value <= rule.Threshold,
                "==" => Math.Abs(value - rule.Threshold) < 0.0001,
                _ => false
            };
        }
    }
}
