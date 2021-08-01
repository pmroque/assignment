using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Services.Models
{
    public class TransactionModel
    {
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }

        public string TransactionDate { get; set; }
        public string Status { get; set; }
    }
}
