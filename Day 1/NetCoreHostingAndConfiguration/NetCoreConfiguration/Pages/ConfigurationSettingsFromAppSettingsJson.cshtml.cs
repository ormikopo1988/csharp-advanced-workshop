using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace NetCoreConfiguration.Pages
{
    public class ConfigurationSettingsFromAppSettingsJsonModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ConfigurationSettingsFromAppSettingsJsonModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ContentResult OnGet()
        {
            // The default JsonConfigurationProvider loads configuration in the following order:
            // 1. appsettings.json
            // 2. appsettings.Environment.json
            // For example, the appsettings.Production.json and appsettings.Development.json files. 
            // The environment version of the file is loaded based on the IHostingEnvironment.EnvironmentName.

            // appsettings.Environment.json values override keys in appsettings.json.
            // For example, by default:
            //  - In development, appsettings.Development.json configuration overwrites values found in appsettings.json.
            //  - In production, appsettings.Production.json configuration overwrites values found in appsettings.json.For example, when deploying the app to Azure.

            // Get config values with IConfiguration
            var myKeyValue = _configuration["MyKey"];
            var title = _configuration["Position:Title"];
            var name = _configuration["Position:Name"];
            var defaultLogLevel = _configuration["Logging:LogLevel:Default"];

            // From array with index
            var firstServerName = _configuration["Servers:0:Name"];
            
            // Casting with (optional) default value
            var country = _configuration.GetValue<string>("Position:Country", "India");

            var primaryConnStr = _configuration.GetConnectionString("PrimaryDB");

            //which is simply a short-hand for
            var secondaryConnStr = _configuration.GetSection("ConnectionStrings")["SecondaryDB"];

            return Content($"MyKey value: {myKeyValue} \n" +
                           $"Title: {title} \n" +
                           $"Name: {name} \n" +
                           $"Default Log Level: {defaultLogLevel} \n" +
                           $"First Server Name: {firstServerName} \n" +
                           $"Position Country: {country} \n" +
                           $"Primary Connection String: {primaryConnStr} \n" +
                           $"Secondary Connection String: {secondaryConnStr}");
        }
    }
}
