using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCoreConfiguration.Models;

namespace NetCoreConfiguration.Pages
{
    public class RegisterConfigAsServiceForInjectionModel : PageModel
    {
        private readonly OAuthSettings _settings;

        public RegisterConfigAsServiceForInjectionModel(OAuthSettings settings)
        {
            _settings = settings;
        }

        public ContentResult OnGet()
        {
            return Content($"ClientId: {_settings.ClientId} \n" +
                           $"Scope: {_settings.Scope} \n" +
                           $"RedirectUrl: {_settings.RedirectUrl} \n");
        }
    }
}
