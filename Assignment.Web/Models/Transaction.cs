﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Web.Models
{
    public class Transaction
    {
            public int Id { get; set; }
            public string TransactionId { get; set; }
            public decimal? Amount { get; set; }
            public string CurrencyCode { get; set; }
            public DateTime? TransactionDate { get; set; }
            public int? StatusId { get; set; }

            public Status Status { get; set; }
       
    }
}
