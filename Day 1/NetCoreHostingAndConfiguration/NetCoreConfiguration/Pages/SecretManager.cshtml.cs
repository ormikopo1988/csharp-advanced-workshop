using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using NetCoreConfiguration.Models;

namespace NetCoreConfiguration.Pages
{
    public class SecretManagerModel : PageModel
    {
        private readonly IConfiguration Configuration;

        public SecretManagerModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ContentResult OnGet()
        {
            var moviesConfig = Configuration.GetSection("Movies")
                                .Get<MovieSettings>();

            return Content($"ConnectionString: {moviesConfig.ConnectionString} \n" +
                       $"ServiceApiKey: {moviesConfig.ServiceApiKey}");
        }
    }
}
