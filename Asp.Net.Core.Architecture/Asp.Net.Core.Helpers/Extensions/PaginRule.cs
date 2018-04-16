using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Extensions
{
    /// <summary>
    /// Objet representant la pagination d'une liste
    /// </summary>
    public class PaginRule
    {
        /// <summary>
        /// Nombre de pages a afficher
        /// </summary>
        public int NombrePages { get; set; }

        /// <summary>
        /// Nombre d'elements par page
        /// </summary>
        public int NombreElementsParPage { get; set; }

        /// <summary>
        /// Nombre total d'items
        /// </summary>
        public int NombreTotalItems { get; set; }

        /// <summary>
        /// Numero de page courante
        /// </summary>
        public int PageCourante { get; set; }
    }
}
