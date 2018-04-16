using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Exceptions
{
    public class CustomRepositoryException : GenericRepositoryException
    {
        public CustomRepositoryException()
        {
        }

        public CustomRepositoryException(string message)
        : base(message)
        {
        }

        public CustomRepositoryException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
