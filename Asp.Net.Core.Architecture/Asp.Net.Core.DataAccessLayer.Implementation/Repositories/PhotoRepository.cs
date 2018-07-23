using Asp.Net.Core.DataAccessLayer.Interface.Repositories;
using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Repositories
{
    public class PhotoRepository : EntityBaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(PhotoGalleryContext context) : base(context)
        {
        }
        public override void Commit()
        {
            base.Commit();
        }
    }
}
