using Asp.Net.Core.Helpers.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Asp.Net.Core.Helpers.Extensions
{
    /// <summary>
    /// Classe d'extension pour filtrer une liste de type IQueryable
    /// </summary>
    public static class QueryableFilterExtension
    {
        private const string TYPE_TEXT = "text";
        private const string TYPE_NUMBER = "number";
        private const string TYPE_DATE = "date";

        /// <summary>
        /// Filter <paramref name="pDataCollection"/> according to <paramref name="pRules"/> sequence
        /// </summary>
        /// <typeparam name="TEntity">Collection item type</typeparam>
        /// <param name="pDataCollection">Queryable collection</param>
        /// <param name="pClasse">Classe a rechercher</param>
        /// <param name="pRules">Filter rules to apply</param>
        /// <returns>Filtered queryable collection</returns>
        public static IQueryable<TEntity> FilterByRules<TEntity>(this IQueryable<TEntity> pDataCollection, IEnumerable<FilterRule> pRules)
        {
            if (pRules.IsNullOrEmpty() || !pRules.Any())
            {
                return pDataCollection;
            }

            // apply first rule
            FilterRule rule = pRules.First();

            var pClasse = typeof(TEntity).Name;

            IQueryable<TEntity> filteredDataCollection = FilterByFieldOrPropertyName(pDataCollection, pClasse, rule.TypeFiltre, rule.Champ, rule.Value);

            // apply next rules recursivly
            return FilterByRulesRecursivly(filteredDataCollection, pClasse, pRules.Skip(1).ToList());
        }

        /// <summary>
        /// Filter <paramref name="pDataCollection"/> according to <paramref name="pRules"/> sequence
        /// </summary>
        /// <typeparam name="TEntity">Collection item type</typeparam>
        /// <param name="pDataCollection">Queryable collection</param>
        /// <param name="pClasse">Classe a rechercher</param>
        /// <param name="pRules">Filter rules to apply</param>
        /// <returns>Filtered queryable collection</returns>
        public static IQueryable<TEntity> FilterByRules<TEntity>(this IQueryable<TEntity> pDataCollection, string pClasse, IEnumerable<FilterRule> pRules)
        {
            if (pRules.IsNullOrEmpty() || !pRules.Any())
            {
                return pDataCollection;
            }

            // apply first rule
            FilterRule rule = pRules.First();

            IQueryable<TEntity> filteredDataCollection = FilterByFieldOrPropertyName(pDataCollection, pClasse, rule.TypeFiltre, rule.Champ, rule.Value);

            // apply next rules recursivly
            return FilterByRulesRecursivly(filteredDataCollection, pClasse, pRules.Skip(1).ToList());
        }

        /// <summary>
        /// Filtre une liste (query LinQ) sur un attribut donne en parametre et selon une valeur
        /// </summary>
        /// <typeparam name="T">Le type de l'objet contenu par la liste</typeparam>
        /// <param name="pDataCollection">La liste a filtrer</param>
        /// <param name="pClasse">Classe a rechercher</param>
        /// <param name="pTypeFiltre">Type de filtre (text, number ou date)</param>
        /// <param name="pChamp">L'attribut sur lequel filtrer</param>
        /// <param name="pValue">Le contenu du filtre</param>
        /// <returns>La query filtree</returns>
        private static IQueryable<TEntity> FilterByFieldOrPropertyName<TEntity>(IQueryable<TEntity> pDataCollection, string pClasse, string pTypeFiltre, ICollection<string> pChamp, object pValue)
        {
            //Recuperation du type de la classe
            Type t = Type.GetType("Carpimko.Models." + pClasse + ", Carpimko.Models, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null");

            if (t == null)
            {
                throw new TableFilterException("Impossible de récupérer le type " + pClasse);
            }

            try
            {
                //Creation de l'identificateur
                ParameterExpression parameter = Expression.Parameter(t, "item");
                //Creation de la constante envoyee par l'IHM
                Expression valueExpression = Expression.Constant(pValue);
                //Acces a la propriete demandee exemple: ["Devise", "Libelle"] : item => item.Devise.Libelle
                Expression propertyExpression = pChamp.Aggregate(parameter, (Expression parent, string path) => Expression.Property(parent, path));

                Expression expressionBoolean = Expression.Constant(true);

                //Filtre sur du text
                if (TYPE_TEXT.Equals(pTypeFiltre))
                {
                    //Recuperation de la methode contains de string
                    MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var valueExpressionToUpper = Expression.Constant(pValue.ToString().ToUpper());
                    var propertyExpressionToUpper = Expression.Call(propertyExpression, "ToUpper", null);
                    //Utilisation de la method
                    expressionBoolean = Expression.Call(propertyExpressionToUpper, containsMethod, valueExpressionToUpper);
                    //Filtre sur des nombres
                }
                else if (TYPE_NUMBER.Equals(pTypeFiltre))
                {
                    expressionBoolean = Expression.Equal(Expression.Convert(propertyExpression, typeof(decimal)), Expression.Convert(valueExpression, typeof(decimal)));
                }
                else if (TYPE_DATE.Equals(pTypeFiltre))
                {
                    //Cast de la value en objet contenant une date de debut et une date de fin
                    FilterValueDate dates = JsonConvert.DeserializeObject<FilterValueDate>(JsonConvert.SerializeObject(pValue));
                    //Traitement sur la date de debut
                    if (dates.DateDebut != null)
                    {
                        expressionBoolean = Expression.And(expressionBoolean, Expression.GreaterThanOrEqual(propertyExpression, Expression.Convert(Expression.Constant(dates.DateDebut.Value), typeof(DateTime))));
                    }
                    //Traitement sur a date de fin
                    if (dates.DateFin != null)
                    {
                        expressionBoolean = Expression.And(expressionBoolean, Expression.LessThanOrEqual(propertyExpression, Expression.Convert(Expression.Constant(dates.DateFin.Value), typeof(DateTime))));
                    }
                }

                Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(expressionBoolean, parameter);

                return pDataCollection.Where(lambda);
            }
            catch (Exception ex)
            {
                throw new TableFilterException("Erreur de filtre " + pTypeFiltre + " pour la classe " + pClasse, ex);
            }
        }

        /// <summary>
        /// Ajoute les regles une par une pour filtrer la liste (query linQ)
        /// </summary>
        /// <typeparam name="T">Le type de l'objet contenu par la liste</typeparam>
        /// <param name="pDataCollection">La liste a filtrer</param>
        /// <param name="pClasse">Classe a rechercher</param>
        /// <param name="pRules">Les regles pour filtrer la liste</param>
        /// <returns>La query filtree</returns>
        private static IQueryable<TEntity> FilterByRulesRecursivly<TEntity>(IQueryable<TEntity> pDataCollection, string pClasse, List<FilterRule> pRules)
        {
            if (!pRules.Any())
            {
                return pDataCollection;
            }

            // apply first rule
            FilterRule rule = pRules.First();
            IQueryable<TEntity> filteredDataCollection = FilterByFieldOrPropertyName(pDataCollection, pClasse, rule.TypeFiltre, rule.Champ, rule.Value);

            // apply next rules recursivly
            return FilterByRulesRecursivly(filteredDataCollection, pClasse, pRules.Skip(1).ToList());
        }
    }
}
