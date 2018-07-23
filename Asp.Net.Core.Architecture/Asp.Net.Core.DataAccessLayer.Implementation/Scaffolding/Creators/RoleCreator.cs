using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Scaffolding.Creators
{
    /// <summary>
    /// Classe de création d'une entité Photo
    /// </summary>
    internal class RoleCreator : BaseCreator
    {
        public List<Role> roles { get; set; }
        public RoleCreator(DbInitializerContext context) : base(context)
        {
            roles = new List<Role>();
        }
        List<Role> _roles = new List<Role>
        {
            new Role(){ Name ="Admin"}

         };

        public override void Create()
        {
            foreach (Role role in _roles)
            {
                CreateRole(role);
            }
        }
        private Role CreateRole(Role role)
        {
            Role result = Context.UnitOfWork.RoleRepository.FindBy(x => x.Name == role.Name).FirstOrDefault();
            if (result == null)
            {
                result = new Role { Name = role.Name };
                Context.UnitOfWork.RoleRepository.Add(result);
                Context.UnitOfWork.Save();
                Context.Logger.Information("Role {Name} ajouté", result.Name);
            }
            roles.Add(result);
            return result;
        }
    }
}
