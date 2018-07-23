using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Scaffolding.Creators
{
    /// <summary>
    /// Classe de création d'une entité Album
    /// </summary>
    internal class AlbumCreator : BaseCreator
    {
        public List<Album> Albums { get; set; }
        public AlbumCreator(DbInitializerContext context) : base(context)
        {
            Albums = new List<Album>();
        }
        List<Album> _albums = new List<Album>
        {
            new Album
            {
                DateCreated = DateTime.Now,
                Title = "Album 1",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            },
            new Album
            {
                DateCreated = DateTime.Now,
                Title = "Album 2",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            },
             new Album
            {
                DateCreated = DateTime.Now,
                Title = "Album 3",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            },
             new Album
            {
                DateCreated = DateTime.Now,
                Title = "Album 4",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            }
        };

        public override void Create()
        {
            foreach (Album _album in _albums)
            {
                CreateAlbum(_album);
            }
        }


        private void CreateAlbum(Album album)
        {
            Album result = Context.UnitOfWork.AlbumRepository.FindBy(x => x.Title == album.Title).FirstOrDefault();
            //Si il existe pas en bdd on le crée
            if (result == null)
            {
                result = new Album { Title = album.Title, Description = album.Description, DateCreated = album.DateCreated };
                Context.UnitOfWork.AlbumRepository.Add(result);
                Context.UnitOfWork.Save();
                Context.Logger.Information("Album {Title} ajoutée", album.Title);
            }
            Albums.Add(result);
        }
    }
}
