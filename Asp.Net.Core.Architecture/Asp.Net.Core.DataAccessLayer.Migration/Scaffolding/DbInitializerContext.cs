using Asp.Net.Core.DataAccessLayer.Interface;
using Asp.Net.Core.Transverse.Logger.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Scaffolding
{
    public class DbInitializerContext
    {
        public DbInitializerContext(
           IUnitOfWork unitOfWork,
           IGenericLogger logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }


        public IUnitOfWork UnitOfWork { get; private set; }
        public IGenericLogger Logger { get; private set; }
    }
}
