using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.Models.Classes
{     
    public class Album : IAlbum
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "playcount")]
        public int PlayCount { get; set; }
        [XmlElement(ElementName = "artist")]
        public Artist ArtistProxy
        {
            get => (Artist)Artist;            
            set => Artist = value;
        }
        [XmlIgnore]
        public IArtist Artist { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
    
    public class TopAlbums : ITopAlbums
    {
        [XmlAttribute(AttributeName = "artist")]
        public string Artist { get; set; }
        [XmlAttribute(AttributeName = "page")]
        public string Page { get; set; }
        [XmlAttribute(AttributeName = "perPage")]
        public string PerPage { get; set; }
        [XmlAttribute(AttributeName = "totalPages")]
        public string TotalPages { get; set; }
        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }
        [XmlElement(ElementName = "album")]
        public List<Album> AlbumsProxy { get; set; }
        [XmlIgnore]
        public IEnumerable<IAlbum> Albums => AlbumsProxy;
        public TopAlbums()
        {
            AlbumsProxy = new List<Album>();
        }
        public TopAlbums(string artist) : this()
        {
            Artist = artist;
            Page = "1";
            PerPage = "10";
        }
    }

}
