using System;
using System.Collections.Generic;
using System.Linq;

namespace Asp.Net.Core.Helpers.Extensions
{
    /// <summary>
    /// Outils génériques
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Vérifie si un objet est null ou vide
        /// </summary>
        /// <typeparam name="T">Type de l'objet</typeparam>
        /// <param name="pData">Objet à tester</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> pData)
        {
            return pData == null || !pData.Any();
        }
    }
}
