using Assignment.Data.Models;
using Assignment.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Assignment.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(AssignmentDBContext context): base(context)
        {
           
        }
        public async Task<IEnumerable<Transaction>> GetByCurrencyCode(string currencyCode)
        {
             return await dbSet.Where(d => d.CurrencyCode == currencyCode).ToListAsync();
           // return await dbSet.ToListAsync();
        }
    }
}
