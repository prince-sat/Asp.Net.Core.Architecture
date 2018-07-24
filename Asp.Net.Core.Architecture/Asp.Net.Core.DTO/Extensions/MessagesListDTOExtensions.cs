using Asp.Net.Core.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DTO.Extensions
{
    public static class MessagesListDTOExtensions
    {
        /// <summary>
        /// Ajout d'un message d'erreur avec son code depuis une valeur d'enum 
        /// </summary>
        /// <typeparam name="TEnum">Le type de l'enum</typeparam>
        /// <param name="messageContainer">Le conteneur des messages</param>
        /// <param name="enumValue">La valeur de l'enum</param>
        /// <param name="args">Les arguments utilisés dans le message (String.Format)</param>
        //public static void AddErrorMessageFromEnum<TEnum>(this MessagesListDTO messageContainer, TEnum enumValue, params string[] args)
        //{
        //    int code = Convert.ToInt32(enumValue);
        //    string message = LocalizedDescriptionHelper.GetLocalizedDescription(enumValue);

        //    if (args != null && args.Length > 0)
        //    {
        //        message = string.Format(message, args);
        //    }

        //    messageContainer.AddErrorMessage(message, code);
        //}
    }
}
