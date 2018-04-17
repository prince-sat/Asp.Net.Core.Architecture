using Asp.Net.Core.DataAccessLayer.Migration.Builders.Base;
using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Builders
{
    internal class UserRoleModelBuilder : BaseBuilder<UserRole>
    {
        protected override void BuildModel(EntityTypeBuilder<UserRole> entity)
        {
            entity.Property(ur => ur.UserId).IsRequired();
            entity.Property(ur => ur.RoleId).IsRequired();
        }
    }
}
