using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DTO.Enums
{
    /// <summary>
    /// Types d'exception
    /// </summary>
    public enum TypeExceptionEnumDTO
    {
        /// <summary>
        /// Exception non définie
        /// </summary>
        Undefined,
        /// <summary>
        /// Exception métié
        /// </summary>
        BusinessException,
        /// <summary>
        /// Exception technique
        /// </summary>
        TechnicalException
    }
}
