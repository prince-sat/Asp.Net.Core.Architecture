using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Asp.Net.Core.DataAccessLayer.Implementation
{
    public static class GestionnaireVersion
    {
        /// <summary>
        /// Récupère la version de l'assembly
        /// </summary>
        public static Tuple<string, string> Version
        {
            get
            {
                string projectAssemblyName = Assembly.GetEntryAssembly().GetName().Name;
                string projectAssemblyVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();

                return new Tuple<string, string>(projectAssemblyName, projectAssemblyVersion);
            }
        }
    }
}
