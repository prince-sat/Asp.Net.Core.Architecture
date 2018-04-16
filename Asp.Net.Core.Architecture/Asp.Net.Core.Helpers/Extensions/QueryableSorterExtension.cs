using Asp.Net.Core.Helpers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asp.Net.Core.Helpers.Extensions
{
    /// <summary>
    /// Classe d'extension pour trier une liste de type IQueryable
    /// </summary>
    public static class QueryableSorterExtension
    {
        /// <summary>
        /// Trier <paramref name="pDataCollection"/> according to <paramref name="pRules"/> sequence
        /// </summary>
        /// <typeparam name="TEntity">Collection item type</typeparam>
        /// <param name="pDataCollection">Queryable collection</param>
        /// <param name="pRule">Sorter rule to apply</param>
        /// <returns>Filtered queryable collection</returns>
        public static IQueryable<TEntity> OrderByRule<TEntity>(this IQueryable<TEntity> pDataCollection, OrderRule pRule = null)
        {
            if (pRule == null)
            {
                return pDataCollection;
            }

            // Applicatrion de la regle
            return OrderByFieldOrPropertyName(pDataCollection, pRule.Champ, pRule.Descending);
        }

        /// <summary>
        /// Tri de la collection par rapport a une regle
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pDataCollection">La collection a trier</param>
        /// <param name="pChamp">Le champ sur lequel la liste doit etre triee</param>
        /// <param name="pDescending">Sens du tri (ASC/DESC)</param>
        /// <returns></returns>
        private static IQueryable<TEntity> OrderByFieldOrPropertyName<TEntity>(IQueryable<TEntity> pDataCollection, ICollection<string> pChamp, bool pDescending)
        {
            //Creation de l'accesseur exemple: Devise.Libelle
            string field = string.Join(".", pChamp);
            //Application du tri
            if (pDescending)
            {
                //Order DESC
                return EntitySorter<TEntity>.OrderByDescending(field).Sort(pDataCollection);
            }
            else
            {
                //Order ASC
                return EntitySorter<TEntity>.OrderBy(field).Sort(pDataCollection);
            }
        }
    }
}
