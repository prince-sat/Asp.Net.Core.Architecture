using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Migration.Scaffolding.Creators
{
    /// <summary>
    /// Classe de création d'une entité Photo
    /// </summary>
    internal class PhotoCreator : BaseCreator
    {
        public AlbumCreator albumCreator { get; set; }
        public List<Photo> photos { get; private set; }
        public PhotoCreator(DbInitializerContext context) : base(context)
        {
            photos = new List<Photo>();
        }
        public override void Create()
        {
            throw new NotImplementedException();
        }
        private Photo CreatePhoto(string image, int _selectedAlbum)
        {
            string _fileName = Path.GetFileName(image);
            Photo result = Context.UnitOfWork.PhotoRepository.FindBy(e => e.Title == _fileName).FirstOrDefault();
            //Si il existe pas en bdd on le crée
            if (result == null)
            {
                result = new Photo
                {
                    Title = _fileName,
                    DateUploaded = DateTime.Now,
                    Uri = _fileName,
                    Album = albumCreator.Albums.ElementAt(_selectedAlbum)
                };
                Context.UnitOfWork.PhotoRepository.Add(result);
                Context.UnitOfWork.Save();
                Context.Logger.Information("Photo {Title} ajoutée", result.Title);

            }
            photos.Add(result);
            return result;


        }

    }
}
