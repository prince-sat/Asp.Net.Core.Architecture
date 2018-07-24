using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SwaggerUITagAttribute : Attribute
    {
        public SwaggerUITagAttribute(string tag)
        {
            Tag = tag;
        }

        public string Tag { get; private set; }
    }
}
