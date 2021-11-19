
using System.Xml.Serialization;

namespace SupportBank
{
    [XmlRoot(ElementName = "Parties")]
    public class Pearent
    {
        [XmlElement("From")]
        public string FromAccount { get; set; }
        [XmlElement("To")]
        public string ToAccount { get; set; }
    }
}
