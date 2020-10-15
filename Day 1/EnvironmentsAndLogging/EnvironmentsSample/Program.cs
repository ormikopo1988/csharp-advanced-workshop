using ClassConventions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace EnvironmentsSample
{
    // Load StartupClassConventions.cs
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        CreateHostBuilder(args).Build().Run();
    //    }

    //    public static IHostBuilder CreateHostBuilder(string[] args)
    //    {
    //        var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;

    //        return Host.CreateDefaultBuilder(args)
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {
    //                webBuilder.UseStartup(assemblyName);
    //            });
    //    }
    //}

    // Either load Startup.cs or StartupInject.cs
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<First.Startup>();
                    webBuilder.UseStartup<Inject.Startup>();
                });
    }
}
