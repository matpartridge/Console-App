using System.Xml.Serialization;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.Models.Classes
{
    public class Artist : IArtist
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
