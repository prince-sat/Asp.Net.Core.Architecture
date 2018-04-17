using Asp.Net.Core.Models.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Models.Models
{
    public class Error : IEntityBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
