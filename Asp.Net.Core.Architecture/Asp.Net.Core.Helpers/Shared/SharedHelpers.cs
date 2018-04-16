using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Asp.Net.Core.Helpers.Shared
{
    /// <summary>
    /// Classe des outils génériques
    /// </summary>
    public static class SharedHelpers
    {
        /// <summary>
        /// Récupère une propriété d'un objet
        /// </summary>
        /// <param name="pObjectType">Type de l'objet</param>
        /// <param name="pPropertyName">Nom de la propriété</param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo(Type pObjectType, string pPropertyName)
        {
            PropertyInfo propertyInfo = pObjectType.GetProperty(pPropertyName);

            return propertyInfo;
        }

        public static string ToPascalCase(string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            string[] wordsResult = new string[words.Count()];

            // Combine the words.
            string result = "";
            int count = 0;

            foreach (string word in words)
            {
                wordsResult[count] =
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1).ToLower();

                count++;
            }
            result = string.Join(" ", wordsResult);
            return result;
        }

    }
}
