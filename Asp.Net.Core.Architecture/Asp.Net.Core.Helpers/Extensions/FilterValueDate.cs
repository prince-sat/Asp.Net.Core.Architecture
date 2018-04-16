using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Extensions
{
    /// <summary>
    /// Objet representant la valeur d'un filtre pour filtrer une date
    /// </summary>
    class FilterValueDate
    {
        /// <summary>
        /// La date de debut du filtre
        /// </summary>
        public DateTime? DateDebut { get; set; }

        /// <summary>
        /// La date de fin du filtre
        /// </summary>
        public DateTime? DateFin { get; set; }
    }
}
