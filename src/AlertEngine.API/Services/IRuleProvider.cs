using AlertEngine.API.Models;

namespace AlertEngine.API.Services
{
    public interface IRuleProvider
    {
        IEnumerable<AlertRule> GetRules();
    }
}
