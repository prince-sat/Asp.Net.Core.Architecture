using Asp.Net.Core.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DTO.Common
{
    public class MessagesListDTO
    {
        public List<MessageDTO> Messages { get; set; }

        public MessagesListDTO()
        {
            Messages = new List<MessageDTO>();
        }

        public void AddSuccessMessage(string content = "", int? code = null)
        {
            Messages.Add(MessageDTO.CreateSuccessMessage(content, code));
        }

        public void AddWarningMessage(string content = "", int? code = null)
        {
            Messages.Add(new MessageDTO(MessageTypeEnumDTO.Warning, content, code));
        }

        public void AddErrorMessage(string content, int? code = null)
        {
            Messages.Add(MessageDTO.CreateErrorMessage(content, code));
        }
    }
}
