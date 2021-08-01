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
        }

        public async Task<IEnumerable<Transaction>> GetByStatus(int statusId)
        {

            return await dbSet.Where(d => d.StatusId == statusId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            return await dbSet.Where(d => d.TransactionDate >= dateFrom && d.TransactionDate <= dateTo).ToListAsync();
        }
    }
}
