using Asp.Net.Core.DataAccessLayer.Migration.Builders.Base;
using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Builders
{
    internal class UserModelBuilder : BaseBuilder<User>
    {
        protected override void BuildModel(EntityTypeBuilder<User> entity)
        {
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            entity.Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            entity.Property(u => u.Salt).IsRequired().HasMaxLength(200);
        }
    }
}
