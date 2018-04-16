using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Extensions
{
    /// <summary>
    /// Regle Filter a effectuer sur un attribut donne (exemple: ["Devise", "Label"])
    /// </summary>
    public class FilterRule
    {
        /// <summary>
        /// Constructeur d'une regle pour le filtre
        /// </summary>
        public FilterRule()
        {
            this.Champ = new List<string>();
            this.Value = null;
            this.TypeFiltre = null;
        }

        /// <summary>
        /// Constructeur d'une regle pour le filtre
        /// </summary>
        /// <param name="pChamp">Nom de l'attribut pour le filtre</param>
        /// <param name="filter">Le filtre. cela peut etre un string dans le cas d'un filtre text ou un objet contenant une date de debut et une date de fin pour les dates.</param>
        /// <param name="pTypeFiltre">Le type de filtre (date / text)</param>
        public FilterRule(ICollection<string> pChamp, object pValue, string pTypeFiltre)
        {
            this.Champ = pChamp;
            this.Value = pValue;
            this.TypeFiltre = pTypeFiltre;
        }

        /// <summary>
        /// Attribut pour la regle filtre
        /// </summary>
        public ICollection<string> Champ { get; set; }

        /// <summary>
        /// Le type de filtre a appliquer
        /// </summary>
        public string TypeFiltre { get; set; }

        /// <summary>
        /// Le filtre
        /// </summary>
        public object Value { get; set; }
    }
}
