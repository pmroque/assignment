using System;
using System.Collections.Generic;

#nullable disable

namespace Assignment.Data.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public decimal? Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? StatusId { get; set; }

        public virtual Status Status{ get; set; }
    }
}
