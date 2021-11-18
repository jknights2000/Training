using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;
namespace SupportBank
{
    
    [XmlType("SupportTransaction")]
    public class Transaction
    {
        [XmlAttribute("Date")]
        public string Date { get; set; }
        
        [XmlElement("Parties")]
        public Pearent p { get; set; }

        [XmlElement("From")]
        public string FromAccount { get; set; }
        [XmlElement("To")]
        public string ToAccount { get; set; }
        [XmlElement("Description")]
        public string Narrative { get; set; }
        [XmlElement("Value")]
        public decimal Amount { get; set; }
        public Transaction()
        {
            this.Date = "";
            this.FromAccount = "";
            this.ToAccount = "";
            this.Narrative = "";
            this.Amount = 0;
        }
        public Transaction (string date,string from,string to,string narritive,decimal amount)
        {
            this.Date = date;
            this.FromAccount = from;
            this.ToAccount = to;
            this.Narrative = narritive;
            this.Amount = amount;
        }
    }

}
