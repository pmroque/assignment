using System;
using System.Collections.Generic;

#nullable disable

namespace Assignment.Data.Models
{
    public partial class Status
    {
        public Status()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string OutputStatus { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
