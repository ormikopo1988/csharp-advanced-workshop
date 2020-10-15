using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Models;

namespace NetCoreConfiguration.Pages
{
    public class OptionsPatternModel : PageModel
    {
        private readonly IConfiguration Configuration;

        public PositionOptions positionOptions { get; private set; }

        private readonly PositionOptions _options;

        public OptionsPatternModel(IConfiguration configuration, IOptions<PositionOptions> options)
        {
            Configuration = configuration;
            _options = options.Value;
        }

        public ContentResult OnGet()
        {
            var positionOptions = new PositionOptions();

            // Bind the PositionOptions class to the Position section of appSettings.json file
            // In the preceding code, by default, changes to the JSON configuration file after the app has started are read.
            //Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

            //return Content($"Title: {positionOptions.Title} \n" +
            //           $"Name: {positionOptions.Name}");

            // ConfigurationBinder.Get<T> binds and returns the specified type.
            // ConfigurationBinder.Get<T> may be more convenient than using ConfigurationBinder.Bind.
            // The following code shows how to use ConfigurationBinder.Get<T> with the PositionOptions class.
            // In the preceding code, by default, changes to the JSON configuration file after the app has started are read
            //positionOptions = Configuration.GetSection(PositionOptions.Position)
            //                                         .Get<PositionOptions>();

            //return Content($"Title: {positionOptions.Title} \n" +
            //           $"Name: {positionOptions.Name}");

            // An alternative approach when using the options pattern is to bind the Position section and add it to the dependency injection service container.
            // In the following code, PositionOptions is added to the service container with Configure and bound to configuration.
            // In the preceding code, changes to the JSON configuration file after the app has started are NOT read.
            // To read changes after the app has started, use IOptionsSnapshot

            return Content($"Title: {_options.Title} \n" +
                       $"Name: {_options.Name}");
        }
    }
}
