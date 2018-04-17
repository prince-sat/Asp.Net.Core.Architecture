using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Builders
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
