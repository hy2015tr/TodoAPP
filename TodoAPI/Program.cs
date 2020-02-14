using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TodoAPI
{
    public class Program
    {
        //---------------------------------------------------------------------------------------------------------------------//

        public static void Main(string[] args)
        {
            CreateHostBuilderV2(args).Build().Run();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public static IWebHostBuilder CreateHostBuilderV1(string[] args) => // Web Host Builder (DotnetCore v2.x)

            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

        //---------------------------------------------------------------------------------------------------------------------//

        public static IHostBuilder CreateHostBuilderV2(string[] args) =>  // Generic Host Builder (DotnetCore v3.x)

            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(x => x.UseStartup<Startup>());

        //---------------------------------------------------------------------------------------------------------------------//

    }
}
