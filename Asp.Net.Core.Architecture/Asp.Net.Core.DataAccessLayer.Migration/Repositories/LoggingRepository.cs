using Asp.Net.Core.DataAccessLayer.Interface.Repositories;
using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Repositories
{
    public class LoggingRepository : EntityBaseRepository<Error>, ILoggingRepository
    {
        public LoggingRepository(PhotoGalleryContext context) : base(context)
        {
        }

      
        public override void Commit()
        {
            base.Commit();
        }

     
    }
}
