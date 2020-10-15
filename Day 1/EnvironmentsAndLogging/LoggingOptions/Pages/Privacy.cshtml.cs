using LoggingOptions.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LoggingOptions.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly IPrivacyService _privacyService;

        public PrivacyModel(ILogger<PrivacyModel> logger, IPrivacyService privacyService)
        {
            _logger = logger;
            _privacyService = privacyService;
        }

        public void OnGet()
        {
            var privacyTerms = _privacyService.GetPrivacyTerms();

            _logger.LogTrace($"Trace {privacyTerms}");
            _logger.LogDebug($"Debug {privacyTerms}");
            _logger.LogInformation($"Information {privacyTerms}");
            _logger.LogWarning($"Warning {privacyTerms}");
            _logger.LogError($"Error {privacyTerms}");
            _logger.LogCritical($"Critical {privacyTerms}");
        }
    }
}
