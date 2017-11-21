using System.Xml.Serialization;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.Models.Classes
{
    [XmlRoot(ElementName = "lfm")]
    public class LastFM : ILastFM

    {
        [XmlAttribute]
        public string status { get; set; }
        [XmlElement(ElementName = "topalbums")]
        public TopAlbums TopAlbumsProxy
        {
            get => (TopAlbums)TopAlbums;
            set => TopAlbums = value;
        }
        [XmlIgnore]
        public ITopAlbums TopAlbums { get; set; }
    }
}
