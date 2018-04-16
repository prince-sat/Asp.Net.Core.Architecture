using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Shared
{
    /// <summary>
    /// Classe d'outils de calculs génériques
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Permet de faire un arrondi suppérieur en précisant le nombre de digits voulu
        /// </summary>
        /// <param name="pValue">decimal a arrondir</param>
        /// <param name="pDigits">nombre de digits pour l'arrondi</param>
        /// <returns>Decimal arrondi</returns>
        public static decimal ArrondirAuSuperieur(decimal pValue, int pDigits)
        {
            decimal valeurArrondie;

            if (pDigits == 0)
            {
                valeurArrondie = Math.Ceiling(pValue);
            }
            else
            {
                valeurArrondie = Convert.ToDecimal(Math.Ceiling(Convert.ToDouble(pValue) * Math.Pow(10, pDigits)) / Math.Pow(10, pDigits));
            }

            return valeurArrondie;
        }
    }
}
