using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer
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
