using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.BusinessLayer.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Retourne l'objet role
        /// </summary>
        /// <param name="roleId">Identifiant du role</param>
        /// <returns></returns>
        Role RolesSelonId(int roleId);
    }
}
