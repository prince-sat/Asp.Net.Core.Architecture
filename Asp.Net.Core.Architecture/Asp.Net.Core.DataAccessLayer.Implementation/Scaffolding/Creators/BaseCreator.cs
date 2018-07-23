using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Scaffolding.Creators
{
    internal abstract class BaseCreator
    {
        protected DbInitializerContext Context;

        public BaseCreator(DbInitializerContext context)
        {
            Context = context;
        }

        public abstract void Create();
    }
}
