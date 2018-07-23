using Asp.Net.Core.DataAccessLayer.Interface.Repositories;
using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Repositories
{
    public class UserRoleRepository : EntityBaseRepository<UserRole>, IUserRoleRepository
    {

        public UserRoleRepository(PhotoGalleryContext context) : base(context)
        {
        }


        public override void Commit()
        {
            base.Commit();
        }
    }
}
