using Asp.Net.Core.BusinessLayer.Habilitation;
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
            InitializeMapper();
        }


        public void Terminating()
        {
            ;
        }
    }
}
