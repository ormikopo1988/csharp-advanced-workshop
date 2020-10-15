using Microsoft.Extensions.Logging;

namespace LoggingOptions.Services
{
    public class PrivacyService : IPrivacyService
    {
        private readonly ILogger<PrivacyService> _logger;

        public PrivacyService(ILogger<PrivacyService> logger)
        {
            _logger = logger;
        }

        public string GetPrivacyTerms()
        {
            _logger.LogTrace($"Trace {nameof(PrivacyService)}.{nameof(GetPrivacyTerms)}()");
            _logger.LogDebug($"Debug {nameof(PrivacyService)}.{nameof(GetPrivacyTerms)}()");
            _logger.LogInformation($"Information {nameof(PrivacyService)}.{nameof(GetPrivacyTerms)}()");
            _logger.LogWarning($"Warning {nameof(PrivacyService)}.{nameof(GetPrivacyTerms)}()");
            _logger.LogError($"Error {nameof(PrivacyService)}.{nameof(GetPrivacyTerms)}()");
            _logger.LogCritical($"Critical {nameof(PrivacyService)}.{nameof(GetPrivacyTerms)}()");

            return "Privacy is a good thing.";
        }
    }
}