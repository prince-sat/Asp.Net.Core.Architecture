using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Extensions
{
    /// <summary>
    /// Classe d'extensions de HostingEnvironment
    /// </summary>
    public static class HostingEnvironmentExtensions
    {
        private const string IntegrationEnvironmentName = "Integration";

        /// <summary>
        /// Détermine si on est en intégration.
        /// La chaine étant "Integration"
        /// </summary>
        /// <param name="pEnv"></param>
        /// <returns></returns>
        public static bool IsIntegration(this IHostingEnvironment pEnv)
        {
            return pEnv.EnvironmentName == IntegrationEnvironmentName;
        }
    }
}
