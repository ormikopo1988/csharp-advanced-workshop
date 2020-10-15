using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace NetCoreConfiguration.Pages
{
    public class DefaultConfigProviderOrderModel : PageModel
    {
        private IConfigurationRoot ConfigRoot;

        public DefaultConfigProviderOrderModel(IConfiguration configRoot)
        {
            ConfigRoot = (IConfigurationRoot)configRoot;
        }

        public ContentResult OnGet()
        {
            // The following code displays the enabled configuration providers in the order they were added

            string str = "";

            var order = 1;

            foreach (var provider in ConfigRoot.Providers.ToList())
            {
                str += $"{order++}. {provider}\n";
            }

            return Content(str);
        }
    }
}
