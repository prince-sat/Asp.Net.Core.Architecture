using Asp.Net.Core.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration
{
    public class PhotoGalleryContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Error> Errors { get; set; }

        public PhotoGalleryContext(DbContextOptions options) : base(options)
        {
        }

        public PhotoGalleryContext() 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            TypeInfo iBuilderTypeInfo = typeof(IBuilder).GetTypeInfo();

            //Récupération de l'ensemble des types des builders
            var entityBuildersTypes = iBuilderTypeInfo.Assembly
                .GetTypes()
                .Where(type =>
                {
                    TypeInfo typeInfo = type.GetTypeInfo();
                    return iBuilderTypeInfo.IsAssignableFrom(type) && !typeInfo.IsAbstract && !typeInfo.IsInterface;
                });

            //Exécution de l'ensemble des builders
            foreach (var entityBuildersType in entityBuildersTypes)
            {
                IBuilder builder = (IBuilder)Activator.CreateInstance(entityBuildersType);
                builder.Build(modelBuilder);
            }

        }

    }
}
