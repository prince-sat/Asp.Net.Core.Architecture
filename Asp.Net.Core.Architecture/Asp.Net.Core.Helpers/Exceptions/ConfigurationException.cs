using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException()
        {
        }

        public ConfigurationException(string message)
        : base(message)
        {
        }

        public ConfigurationException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
