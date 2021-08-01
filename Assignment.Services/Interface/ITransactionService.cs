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

        IEnumerable<Transaction> GetTransactionsAll();

        IEnumerable<Transaction> GetTransactionsByCurrency(string currency);

        IEnumerable<Transaction> GetTransactionsByDateRange(DateTime dateFrom, DateTime dateTo);

        IEnumerable<Transaction> GetTransactionsByStatus(string status);
    }
}
