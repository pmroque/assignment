using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Assignment.Services.Models
{

    [XmlRoot("Transactions")]
    public class TransactionsXML
    {

        [XmlElement("Transaction")]
        public List<TransactionXML> Transaction { get; set; }
    }

    [XmlType("Transaction")]
    public class TransactionXML
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("TransactionDate")]
        public string TransactionDate { get; set; }

        [XmlElement("PaymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }
    }



    [XmlType("PaymentDetails")]
    public class PaymentDetails
    {
        [XmlElement("Amount")]
        public string Amount { get; set; }
        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }
    }


}
