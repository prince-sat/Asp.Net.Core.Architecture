using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.DTO.PhotoGallery
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }

        public DateTime DateUploaded { get; set; }
    }
}
