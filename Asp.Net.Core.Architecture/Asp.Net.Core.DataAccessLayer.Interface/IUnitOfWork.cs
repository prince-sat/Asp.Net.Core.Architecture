using Asp.Net.Core.DataAccessLayer.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DataAccessLayer.Interface
{
    public interface IUnitOfWork
    {
        ILoggingRepository ErrorRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
    }
}
