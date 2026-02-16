using AlertEngine.API.Models;

namespace AlertEngine.API.Services
{
    public class RuleEvaluator
    {
        private readonly IRuleProvider _ruleProvider;

        public RuleEvaluator(IRuleProvider ruleProvider)
        {
            _ruleProvider = ruleProvider;
        }

        public IEnumerable<AlertRule> Evaluate(AlertEvent alertEvent)
        {
            var rules = _ruleProvider.GetRules();

            return rules
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
