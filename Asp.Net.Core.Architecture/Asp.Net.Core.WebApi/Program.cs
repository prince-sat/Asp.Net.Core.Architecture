using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asp.Net.Core.Transverse.Logger.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace Asp.Net.Core.WebApi
{
    public class Program
    {
        const string EnvironmentKey = "MYPROJECT_ENVIRONMENT";

        /// <summary>
        /// Méthode principale
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = BuildConfiguration();
            SerilogLoggerBuilderHelper.BuildSerilogLogger(configuration[EnvironmentKey]);
            IWebHost host = BuildWebHost(configuration);

            host.Run();
        }

        /// <summary>
        /// Build the web host
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static IWebHost BuildWebHost(IConfigurationRoot config)
        {
            return new WebHostBuilder()
               .UseConfiguration(config)
               .UseKestrel()
               .UseEnvironment(config[EnvironmentKey])
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseStartup<Startup>()
               .UseSerilog()
               .UseApplicationInsights()
               .Build();
        }

        /// <summary>
        /// Build the configuration
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Environment.json", optional: true)
                .Build();
        }
    }
}
