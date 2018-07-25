using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.WebApi.Mapping
{
    /// <summary>
    /// Classe permettant d'avoir des outils pour configurer les mappings
    /// </summary>
    public class MappingHelper : Profile
    {
        private MapperConfigurationExpression mapperConf = new MapperConfigurationExpression();

        public MapperConfigurationExpression MapperConfiguration
        {
            get
            {
                return mapperConf;
            }

            set
            {
                mapperConf = value;
            }
        }

        /// <summary>
        /// Applique un mapping sur les deux types fournis en paramètre en mode bidirectionnel.
        /// </summary>
        /// <typeparam name="TSource">Type source à mapper</typeparam>
        /// <typeparam name="TDestination">Type destination à mapper</typeparam>
        protected void TwoWayMapping<TSource, TDestination>()
        {
            OneWayMapping<TSource, TDestination>();
            OneWayMapping<TDestination, TSource>();
        }

        /// <summary>
        /// Applique un mapping sur les deux types fournis en paramètre en mode unidirectionnel.
        /// </summary>
        /// <typeparam name="TSource">Type source à mapper</typeparam>
        /// <typeparam name="TDestination">Type destination à mapper</typeparam>
        protected IMappingExpression<TSource, TDestination> OneWayMapping<TSource, TDestination>()
        {
            IMappingExpression<TSource, TDestination> mapping = MapperConfiguration
                .CreateMap<TSource, TDestination>();
            return mapping;
        }

        /// <summary>
        /// Méthode d'initialisation d'automapper
        /// </summary>
        public void InitializeMapper()
        {
            Mapper.Initialize(MapperConfiguration);
        }
    }
}
