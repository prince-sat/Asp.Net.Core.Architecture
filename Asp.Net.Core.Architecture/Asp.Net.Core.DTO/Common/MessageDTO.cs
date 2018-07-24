using Asp.Net.Core.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asp.Net.Core.DTO.Common
{
    /// <summary xml:lang="fr">
    /// Classe qui représente un message avec un contenu et un niveau de priorité.
    /// </summary>
    public class MessageDTO
    {
        /// <summary xml:lang="fr">
        /// Le niveau d'importance du message.
        /// </summary>
        public MessageTypeEnumDTO Level { get; set; }

        /// <summary xml:lang="fr">
        /// Le contenu du message.
        /// </summary>
        public String Content { get; set; }

        /// <summary xml:lang="fr">
        /// Le code du message.
        /// </summary>
        public int? Code { get; set; }

        /// <summary xml:lang="fr">
        /// L'exception en cas d'erreur.
        /// </summary>
        public ExceptionDTO ExceptionError { get; set; }

        public MessageDTO()
        {

        }

        public MessageDTO(MessageTypeEnumDTO level, String content, int? code = null)
        {
            Level = level;
            Content = content;
            Code = code;
        }

        public MessageDTO(MessageTypeEnumDTO level, String content, Exception ex, int? code = null)
        {
            Level = level;
            Content = content;
            ExceptionError = new ExceptionDTO(string.Empty, ex);
            Code = code;
        }

        public bool IsSuccess()
        {
            return this.Level == MessageTypeEnumDTO.Success;
        }

        public bool IsNotSuccess()
        {
            return this.Level != MessageTypeEnumDTO.Success;
        }

        public static MessageDTO CreateSuccessMessage(string content = "", int? code = null)
        {
            return new MessageDTO(MessageTypeEnumDTO.Success, content, code);
        }

        public static MessageDTO CreateErrorMessage(string content, int? code = null)
        {
            return new MessageDTO(MessageTypeEnumDTO.Error, content, code);
        }

        /// <summary xml:lang="fr">
        /// Permet de récupérer le niveau d'importance le plus élevé qui existe dans les messages envoyés en paramètre.
        /// </summary>
        /// <param xml:lang="fr" name="messages">La liste des messages à explorer.</param>
        /// <returns xml:lang="fr">Le niveau d'importance le plus élevé.</returns>
        public static MessageTypeEnumDTO GetMostSignificantMessageLevel(IEnumerable<MessageDTO> messages)
        {
            int highestMessagePriority = (int)MessageTypeEnumDTO.Unknown;
            foreach (MessageDTO message in messages)
            {
                int currentMessagePriority = (int)message.Level;
                if (highestMessagePriority < currentMessagePriority)
                {
                    highestMessagePriority = currentMessagePriority;
                }
            }

            return (MessageTypeEnumDTO)highestMessagePriority;
        }

        /// <summary xml:lang="fr">
        /// Permet de récupérer le premier message qui a le niveau d'importance le plus élevé dans une liste.
        /// </summary>
        /// <param xml:lang="fr" name="messages">La liste des messages à explorer.</param>
        /// <returns xml:lang="fr">Le premier message de plus haute importance ou null s'il n'existe pas de message.</returns>
        public static MessageDTO FindFirstMostSignificantMessage(IEnumerable<MessageDTO> messages)
        {
            MessageTypeEnumDTO highestMessagePriority = GetMostSignificantMessageLevel(messages);
            return messages.FirstOrDefault(e => e.Level == highestMessagePriority);
        }
    }
}
