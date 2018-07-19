using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer
{
    internal class ErrorModelBuilder : BaseBuilder<Error>
    {
        protected override void BuildModel(EntityTypeBuilder<Error> entity)
        {

        }
    }
}
