using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer
{
    internal class AlbumModelBuilder : BaseBuilder<Album>
    {
        protected override void BuildModel(EntityTypeBuilder<Album> entity)
        {
            entity.Property(a => a.Title).HasMaxLength(100);
            entity.Property(a => a.Description).HasMaxLength(500);
            entity.HasMany(a => a.Photos).WithOne(p => p.Album);
        }
    }
}
