using Asp.Net.Core.DataAccessLayer.Migration.Scaffolding.Creators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Scaffolding
{
    /// <summary>
    /// Initialisation des données
    /// </summary>
    public class ScaffoldingDbInitializer
    {

        public ScaffoldingDbInitializer()
        {

        }
        /// <summary>
        /// Initialisation des données
        /// </summary>
        /// <param name="context">Contexte d'initialisation</param>
        public void Initialize(DbInitializerContext context, string _applicationPath)
        {
            context.Logger.Information("[Scaffolding] - Début de l'initialisation de la base de données");

            //Initialisation des creators
            AlbumCreator albumCreator = new AlbumCreator(context);
            PhotoCreator photoCreator = new PhotoCreator(context, _applicationPath);
            RoleCreator roleCreator = new RoleCreator(context);
            UserCreator userCreator = new UserCreator(context);
            UserRoleCreator userRoleCreator = new UserRoleCreator(context);

            //AlbumCreator: Création des données 
            albumCreator.Create();

            //PhotoCreator : Création des données 
            photoCreator.albumCreator = albumCreator;
            photoCreator.Create();

            //RoleCrearor : Création des données
            roleCreator.Create();

            //UserCreator : Création des données 
            userCreator.Create();

            //UserRoleCreator : Création des données 
            userRoleCreator.Role = roleCreator.roles.FirstOrDefault(e => e.Name == "Admin");
            userRoleCreator.Users = userCreator.Users;
            userRoleCreator.Create();


            context.Logger.Information("[Scaffolding] - Fin de l'initialisation de la base de données");
        }
    }
}
