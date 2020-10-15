using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace NetCoreConfiguration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var envVarEnumerator = Environment.GetEnvironmentVariables().GetEnumerator();
            
            while (envVarEnumerator.MoveNext())
            {
                _logger.LogInformation($"{envVarEnumerator.Key,5}:{envVarEnumerator.Value,100}");
            }
        }
    }
}
