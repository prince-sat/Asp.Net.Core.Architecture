﻿using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Builders
{

    internal class PhotoModelBuilder : BaseBuilder<Photo>
    {
        protected override void BuildModel(EntityTypeBuilder<Photo> entity)
        {

            entity.Property(p => p.Title).HasMaxLength(100);
            entity.Property(p => p.AlbumId).IsRequired();
        }
    }
       
}