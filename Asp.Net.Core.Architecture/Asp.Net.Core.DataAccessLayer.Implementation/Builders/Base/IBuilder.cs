using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Builders
{
    public interface IBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
