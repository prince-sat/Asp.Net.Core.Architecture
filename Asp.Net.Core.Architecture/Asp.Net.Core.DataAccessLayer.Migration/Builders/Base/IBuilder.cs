using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Builders.Base
{
    public interface IBuilder
    {
       void Build(ModelBuilder modelBuilder);
    }
}
