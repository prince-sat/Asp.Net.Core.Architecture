using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Extensions
{
    /// <summary>
    /// Regle OrderBy a effectuer sur un attribut donne
    /// </summary>
    public class OrderRule
    {
        /// <summary>
        /// Constructeur d'une regle pour le OrderBy
        /// </summary>
        public OrderRule()
        {
            this.Champ = new List<string>();
            this.Descending = false;
        }

        /// <summary>
        /// Constructeur d'une regle pour le OrderBy
        /// </summary>
        /// <param name="pFieldOrPropertyName">Nom de l'attribut pour le OrderBy</param>
        /// <param name="pDescending">Flag descending (ASC/DESC)</param>
        public OrderRule(ICollection<string> pChamp, bool pDescending)
        {
            this.Champ = pChamp;
            this.Descending = pDescending;
        }

        /// <summary>
        /// Attribut pour la regle OrderBy
        /// </summary>
        public ICollection<string> Champ { get; set; }

        /// <summary>
        /// Flag descending (ASC/DESC)
        /// </summary>
        public bool Descending { get; set; }
    }
}
