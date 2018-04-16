using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Exceptions
{
    public class ControllerException : Exception
    {
        public ControllerException()
        {
        }

        public ControllerException(string message)
        : base(message)
        {
        }

        public ControllerException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
