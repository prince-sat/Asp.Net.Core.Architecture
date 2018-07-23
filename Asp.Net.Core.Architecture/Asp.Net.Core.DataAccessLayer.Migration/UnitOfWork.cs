using Asp.Net.Core.DataAccessLayer.Interface;
using Asp.Net.Core.DataAccessLayer.Interface.Repositories;
using Asp.Net.Core.DataAccessLayer.Migration.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration
{
    /// <summary>
    /// Classe d'accès au context Db
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        /// <summary>
        /// Context pour accéder à la base de données
        /// </summary>
        private readonly PhotoGalleryContext _dbContext;

        /**
        * Déclaration des Service d'accès aux modeles
        **/
        /// <summary>  
        /// producteur des services en utilisant l'injection des dépendances  
        /// </summary>  
        private readonly IServiceProvider ServiceProvider;

        #region Constructeurs

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="dbContext">Classe d'accès à la base de données</param>
        public UnitOfWork(PhotoGalleryContext dbContext)
        {
            _dbContext = dbContext;
            LoggingRepository = new LoggingRepository(dbContext);
            AlbumRepository = new AlbumRepository(dbContext);
            PhotoRepository = new PhotoRepository(dbContext);
            RoleRepository = new RoleRepository(dbContext);
            UserRepository = new UserRepository(dbContext, RoleRepository);
            UserRoleRepository = new UserRoleRepository(dbContext);

        }

        /// <summary>
        /// Un constructeur avec l'injection de contexte et le producteur des service
        /// </summary>
        /// <param name="pContext"> Classe d'accès à la base de données</param>
        /// <param name="serviceProvider">Producteur des services (Injection de dépendance)</param>
        public UnitOfWork(PhotoGalleryContext pContext, IServiceProvider serviceProvider)
            : this(pContext)
        {
            ServiceProvider = serviceProvider;
        }


        #endregion


        #region Repositories

        public ILoggingRepository LoggingRepository { get; }

        public IAlbumRepository AlbumRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }

        /// <summary>
        /// Sauvegarde en base
        /// </summary>
        /// <returns>Code de retour</returns>
        public int Save()
        {
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        /// Sauvegarde asynchrone en base
        /// </summary>
        /// <returns>Code de retour</returns>
        public Task<int> SaveAsync()
        {
            return this._dbContext.SaveChangesAsync();
        }


        #endregion



    }
}
