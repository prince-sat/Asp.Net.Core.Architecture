using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Builders
{
    internal class ErrorModelBuilder : BaseBuilder<Error>
    {
        protected override void BuildModel(EntityTypeBuilder<Error> entity)
        {

        }
    }
}
