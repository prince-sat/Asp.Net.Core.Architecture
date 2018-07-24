using Asp.Net.Core.DTO.Common;
using Asp.Net.Core.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.WebApi.Responses
{
    /// <summary>
    /// Cette classe sert d'encapsulation au retour de méthode de service web
    /// </summary>
    /// <typeparam xml:lang="fr" name="T">Type de l'information</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="result"></param>
        public Response()
        {
            Messages = new MessagesListDTO();
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="result"></param>
        public Response(T result)
            : this()
        {
            Result = result;
        }

        /// <summary>
        /// Résultat de la requête
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Messages retournés.
        /// </summary>
        public MessagesListDTO Messages { get; set; }

        /// <summary>
        /// Si il existe des messages d'erreur
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return Messages.Messages
                    .FirstOrDefault(x => x.Level == MessageTypeEnumDTO.Error || x.Level == MessageTypeEnumDTO.Fatal) != null;
            }
        }
    }
}
