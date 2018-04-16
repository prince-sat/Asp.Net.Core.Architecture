using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Exceptions
{
    public class DbContextException : Exception
    {
        public DbContextException()
        {
        }

        public DbContextException(string message)
        : base(message)
        {
        }

        public DbContextException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
