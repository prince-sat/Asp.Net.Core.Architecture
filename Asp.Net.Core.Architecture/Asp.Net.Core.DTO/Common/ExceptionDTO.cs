using Asp.Net.Core.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DTO.Common
{
    public class ExceptionDTO
    {
        /// <summary>
        /// Message à diffuser à l'extérieur concernant l'exception.
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// Identifiant de l'exception.
        /// </summary>
        public string Id { get; protected set; }

        /// <summary>
        /// Type de l'exception.
        /// </summary>
        public TypeExceptionEnumDTO Type { get; protected set; }

        /// <summary>
        /// Exception source.
        /// </summary>
        public ExceptionDTO InnerException { get; set; }

        /// <summary>
        /// Stack trace de l'exception.
        /// </summary>
        public string StackTrace { get; protected set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="id">Identifiant de l'exception</param>
        /// <param name="ex">Exception</param>
        public ExceptionDTO(string id, Exception ex)
        {
            this.Message = ex.Message;
            this.Id = id;
            this.Type = TypeExceptionEnumDTO.Undefined;
            this.InnerException = null;
            this.StackTrace = ex.StackTrace;
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="message">Message de l'exception qui est transmis
        /// aux systèmes externes</param>
        /// <param name="id">Identifiant de l'exception</param>
        /// <param name="type">Type de l'exception</param>
        /// <param name="stackTrace">stackTrace de l'exception</param>
        public ExceptionDTO(string message, string id,
            TypeExceptionEnumDTO type, string stackTrace)
        {
            this.Message = message;
            this.Id = id;
            this.Type = type;
            this.InnerException = null;
            this.StackTrace = stackTrace;
        }

        /// <summary>
        /// On surcharge la méthode ToString() pour la rendre plus pertinente.
        /// </summary>
        public override string ToString()
        {
            return "Message exception : " + Message;
        }
    }
}
