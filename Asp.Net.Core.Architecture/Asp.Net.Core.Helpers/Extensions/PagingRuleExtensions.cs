using Asp.Net.Core.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asp.Net.Core.Helpers.Extensions
{
    public static class PagingRuleExtensions
    {
        /// <summary>
        /// Initialise la pagination
        /// </summary>
        /// <param name="paginRule"></param>
        public static void Initialize(this PaginRule paginRule)
        {
            if (paginRule.PageCourante == 0)
                paginRule.PageCourante = 1;

            if (paginRule.NombreElementsParPage == 0)
                paginRule.NombreElementsParPage = PaginationConstantes.NOMBRE_ELEMENTS_PAR_PAGE;
        }

        /// <summary>
        /// Remplie l'ensemble des paramètres nécessaire à la pagination
        /// </summary>
        /// <param name="paginRule"></param>
        public static void FillParametersFromCollection<TObject>(this PaginRule paginRule, IEnumerable<TObject> items)
        {
            int nombreTotalElements = items.Count();

            paginRule.NombreTotalItems = nombreTotalElements;
            double nombrePages = nombreTotalElements / (double)paginRule.NombreElementsParPage;
            if (nombrePages % 1 > 0)
                nombrePages++;

            paginRule.NombrePages = (int)nombrePages;
        }

        /// <summary>
        /// Filtre une collection d'éléments selon la règle de pagination
        /// </summary>
        /// <param name="paginRule"></param>
        public static List<TObject> FilterCollection<TObject>(this PaginRule paginRule, IEnumerable<TObject> items)
        {
            int skip = paginRule.NombreElementsParPage * (paginRule.PageCourante - 1);

            //Filtrage des données selon la pagination
            List<TObject> parametresApplicatifs = items.Skip(skip)
                                              .Take(paginRule.NombreElementsParPage)
                                              .ToList();

            return parametresApplicatifs;
        }
    }
}
