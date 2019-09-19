using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TodoAPI
{
    public class Program
    {
        //---------------------------------------------------------------------------------------------------------------------//

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //---------------------------------------------------------------------------------------------------------------------//

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

        //---------------------------------------------------------------------------------------------------------------------//

    }
}
