using AlertEngine.API.Models;
using System.Text.Json;

namespace AlertEngine.API.Services
{
    public class FileRuleProvider : IRuleProvider
    {
        private readonly string _filePath;
        private List<AlertRule> _rules = new();

        public FileRuleProvider(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Rules", "rules.json");
            LoadRules();

            // 🔥 File watcher (preparing for ConfigMap reload in AKS)
            var watcher = new FileSystemWatcher(Path.GetDirectoryName(_filePath)!)
            {
                Filter = "rules.json",
                EnableRaisingEvents = true
            };

            watcher.Changed += (_, __) => LoadRules();
        }

        private void LoadRules()
        {
            if (!File.Exists(_filePath))
                return;

            var json = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var rules = JsonSerializer.Deserialize<List<AlertRule>>(json, options);

            if (rules != null)
            {
                _rules = rules;
                Console.WriteLine("Rules reloaded successfully");
            }
        }

        public IEnumerable<AlertRule> GetRules() => _rules;
    }
}
