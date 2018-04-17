using Asp.Net.Core.Models.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Models.Models
{
    public class Photo : IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public virtual Album Album { get; set; }
        public int AlbumId { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}
