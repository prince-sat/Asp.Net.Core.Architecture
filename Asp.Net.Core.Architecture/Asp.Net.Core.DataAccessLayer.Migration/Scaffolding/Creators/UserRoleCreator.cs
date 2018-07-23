using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Scaffolding.Creators
{
    /// <summary>
    /// Classe de création d'une entité Photo
    /// </summary>
    internal class UserRoleCreator : BaseCreator
    {
        public List<User> Users { get; set; }
        public Role Role { get; set; }
        public UserRoleCreator(DbInitializerContext context) : base(context)
        {
            Users = new List<User>();
            Role = new Role();
        }

        public override void Create()
        {
            foreach (User user in Users)
            {
                UserRole result = Context.UnitOfWork.UserRoleRepository.
                    FindBy(e => e.RoleId == Role.Id && e.UserId == user.Id).FirstOrDefault();
                if (result == null)
                    CreateUserRole(user, Role);
            }
        }
        private UserRole CreateUserRole(User user, Role role)
        {
            UserRole result = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            Context.UnitOfWork.UserRoleRepository.Add(result);
            return result;
        }
    }
}
