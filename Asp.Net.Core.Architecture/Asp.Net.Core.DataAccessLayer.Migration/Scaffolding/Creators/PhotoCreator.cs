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
        public string[] Images { get; set; }

        public PhotoCreator(DbInitializerContext context, string applicationPath) : base(context)
        {
            photos = new List<Photo>();
            Images = Directory.GetFiles(Path.Combine(applicationPath, "images"));
        }
        public override void Create()
        {
            foreach (string _image in Images)
            {
                int _selectedAlbum = rnd.Next(1, 4);
                CreatePhoto(_image, _selectedAlbum);
            }
        }

        Random rnd = new Random();
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
                    AlbumId = albumCreator.Albums.ElementAt(_selectedAlbum).Id
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
