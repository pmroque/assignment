using Assignment.Data.Models;
using Assignment.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;

namespace Assignment.Services.Interface
{
    public interface ITransactionService
    {
        ResponseResult Upload( IFormFile file);

        IEnumerable<TransactionModel> GetTransactionsAll();

        IEnumerable<TransactionModel> GetTransactionsByCurrency(string currency);

        IEnumerable<TransactionModel> GetTransactionsByDateRange(DateTime dateFrom, DateTime dateTo);

        IEnumerable<TransactionModel> GetTransactionsByStatus(string status);
    }
}
