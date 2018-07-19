using Asp.Net.Core.DataAccessLayer.Interface.Repositories;
using Asp.Net.Core.Models.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Interface
{
    public interface IUnitOfWork
    {
        ILoggingRepository ErrorRepository { get; }

        IAlbumRepository AlbumRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }

        /// <summary>
        /// Sauvegarde en base
        /// </summary>
        /// <returns>Code de retour</returns>
        int Save();
        /// <summary>
        /// Sauvegarde asynchrone en base
        /// </summary>
        /// <returns>Code de retour</returns>
        Task<int> SaveAsync();

    }
}
