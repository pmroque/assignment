using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Repositories.Interface
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {

        Task<IEnumerable<Transaction>> GetAll();

        Task<IEnumerable<Transaction>> GetByCurrencyCode(string currencyCode);

        Task<IEnumerable<Transaction>> GetByStatus(int statusId);

        Task<IEnumerable<Transaction>> GetTransactionsByDateRange(DateTime dateFrom, DateTime dateTo);

    }
}
