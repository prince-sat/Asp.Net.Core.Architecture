using Asp.Net.Core.DTO.Habilitation;
using Asp.Net.Core.DTO.PhotoGallery;
using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.WebApi.Mapping
{
    public sealed class MappingDeclaration : MappingHelper
    {

        public void Activated()
        {
            // Déclaration des éléments à mapper dans l'ordre alphabétique.
            OneWayMapping<Role, RoleDTO>();
            OneWayMapping<RoleDTO, Role>();

            OneWayMapping<Photo, PhotoDTO>();
            OneWayMapping<PhotoDTO, Photo>();

            OneWayMapping<Album, AlbumDTO>();
            OneWayMapping<AlbumDTO, Album>();

            InitializeMapper();
        }


        public void Terminating()
        {
            ;
        }
    }
}
