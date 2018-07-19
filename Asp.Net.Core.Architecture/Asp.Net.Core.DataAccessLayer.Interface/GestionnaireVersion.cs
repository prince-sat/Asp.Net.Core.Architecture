using System;
using System.Reflection;

namespace Asp.Net.Core.DataAccessLayer.Interface
{
    /// <summary>
    /// Classe de gestion des version de l'assembly
    /// </summary>
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
