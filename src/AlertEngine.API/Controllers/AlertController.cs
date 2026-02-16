using AlertEngine.API.Models;
using AlertEngine.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AlertEngine.API.Controllers
{
    [Route("api/alerts")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly RuleEvaluator _ruleEvaluator;
        private readonly List<AlertRule> _rules;

        public AlertController()
        {
            var json = System.IO.File.ReadAllText("Rules/rules.json");
            //_rules = JsonSerializer.Deserialize<List<AlertRule>>(json)!;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            _rules = JsonSerializer.Deserialize<List<AlertRule>>(json, options)!;

            _ruleEvaluator = new RuleEvaluator(_rules);
        }

        [HttpPost("evaluate")]
        public IActionResult Evaluate([FromBody] AlertEvent alertEvent)
        {
            var triggeredRules = _ruleEvaluator.Evaluate(alertEvent);

            return Ok(new
            {
                alertEvent,
                triggeredAlerts = triggeredRules
            });
        }

        [HttpGet("rules")]
        public IActionResult GetRules()
        {
            return Ok(_rules);
        }
    }
}
