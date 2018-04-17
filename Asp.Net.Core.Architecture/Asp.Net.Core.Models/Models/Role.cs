using Asp.Net.Core.Models.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Models.Models
{
    public class Role : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
