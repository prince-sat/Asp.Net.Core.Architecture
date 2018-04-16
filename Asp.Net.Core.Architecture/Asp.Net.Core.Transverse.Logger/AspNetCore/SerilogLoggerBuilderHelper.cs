using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;

namespace Asp.Net.Core.Transverse.Logger.AspNetCore
{
    public static class SerilogLoggerBuilderHelper
    {


        public static void BuildSerilogLogger(string environment)
        {
            var serilogConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"serilog.{environment}.json")
                .Build();

            var logger = new LoggerConfiguration()
               .ReadFrom.Configuration(serilogConfig)
               .CreateLogger();

            Log.Logger = logger;
        }
    }
}
