using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Implementation
{
    /// <summary>
    /// Classe de base pour les builders
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    internal abstract class BaseBuilder<TModel> : IBuilder
        where TModel : class
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TModel>(entity =>
            {
                BuildModel(entity);
            });
        }

        /// <summary>
        /// Fonction de construction du modèle
        /// </summary>
        /// <param name="entity"></param>
        protected abstract void BuildModel(EntityTypeBuilder<TModel> entity);
    }
}
