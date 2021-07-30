using Assignment.Data.Models;
using Assignment.Repositories.Interface;
using Assignment.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class TransactionService: ITransactionService
    {
        private string[] VALID_EXTENSIONS = { "csv", "xml" };

        private protected ITransactionRepository  transactionRepository { get; set; }

        public TransactionService(ITransactionRepository _repository)
        {
            transactionRepository = _repository;
        }

        public HttpStatusCode Upload(IFormFile file)
        {
            if(CheckIfValidFile(file.FileName))
            {
                return HttpStatusCode.BadRequest;
            }
          
            return HttpStatusCode.OK;
        }

        public IEnumerable<Transaction> GetTransactionsById(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionsByCurrency(string currency)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionsByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionsByStatus(string status)
        {
            throw new NotImplementedException();
        }



        private bool CheckIfValidFile(string fileName)
        {           
            var extension = fileName.Split('.')[fileName.Split('.').Length - 1];

            return VALID_EXTENSIONS.Contains(extension); 
        }

    }
}
