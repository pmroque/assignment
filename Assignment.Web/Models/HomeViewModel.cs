using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Web.Models
{
    public class HomeViewModel
    {
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();        
    }
    
    public class TransactionViewModel
    {
        public string Id { get; set; }
        public string Payment { get; set; }       
        public string Status { get; set; }
    }
}
