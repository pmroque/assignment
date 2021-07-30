using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Repositories.Interface
{
    public interface ITransactionRepository: IGenericRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByCurrencyCode(string currencyCode);
    }
}
