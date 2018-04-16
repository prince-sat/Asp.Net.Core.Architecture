using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Exceptions
{
    public class TableFilterException : Exception
    {
        public TableFilterException()
        {
        }

        public TableFilterException(string message)
        : base(message)
        {
        }

        public TableFilterException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }

    public class TableSortException : Exception
    {
        public TableSortException()
        {
        }

        public TableSortException(string message)
        : base(message)
        {
        }

        public TableSortException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
