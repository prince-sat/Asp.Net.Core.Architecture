﻿using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Implementation
{
    internal class RoleModelBuilder : BaseBuilder<Role>
    {
        protected override void BuildModel(EntityTypeBuilder<Role> entity)
        {
            entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
        }
    }
}
