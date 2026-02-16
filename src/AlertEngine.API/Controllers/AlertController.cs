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
        private readonly IRuleProvider _ruleProvider;

        public AlertController(RuleEvaluator ruleEvaluator, IRuleProvider ruleProvider)
        {
            _ruleEvaluator = ruleEvaluator;
            _ruleProvider = ruleProvider;
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
            return Ok(_ruleProvider.GetRules());
        }
    }
}
