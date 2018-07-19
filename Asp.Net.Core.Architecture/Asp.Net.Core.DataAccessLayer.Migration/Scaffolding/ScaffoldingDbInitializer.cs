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
        public void Initialize(DbInitializerContext context)
        {
            context.Logger.Information("[Scaffolding] - Début de l'initialisation de la base de données");

            //Initialisation des creators
            AlbumCreator albumCreator = new AlbumCreator(context);
            PhotoCreator photoCreator = new PhotoCreator(context);
            RoleCreator roleCreator = new RoleCreator(context);
            UserCreator userCreator = new UserCreator(context);
            UserRoleCreator userRoleCreator = new UserRoleCreator(context);

            //AlbumCreator: Création des données 
            albumCreator.Create();


            context.Logger.Information("[Scaffolding] - Fin de l'initialisation de la base de données");
        }
    }
}
