using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Exceptions
{
    public class GenericRepositoryException : Exception
    {
        public GenericRepositoryException()
        {
        }

        public GenericRepositoryException(string message)
        : base(message)
        {
        }

        public GenericRepositoryException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
