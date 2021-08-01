using Assignment.Data.Models;
using Assignment.Repositories.Interface;
using Assignment.Services.Helpers;
using Assignment.Services.Interface;
using Assignment.Services.Models;
using Assignment.Services.Uploader;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace Assignment.Services
{
    public class TransactionService : ITransactionService
    {



        private protected ITransactionRepository transactionRepository { get; set; }
        public ILogger logger { get; set; }

        public TransactionService(ITransactionRepository _repository, ILogger<TransactionService> _logger)
        {
            transactionRepository = _repository;
            logger = _logger;
            logger.LogInformation("pol");
        }

        public ResponseResult Upload(IFormFile file)
        {
            IUploader uploader;

            var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            switch (extension)
            {
                case "xml": uploader = new XmlUploder(transactionRepository, logger); break;
                default: //CSV is the default uploader
                    uploader = new CsvUploader(transactionRepository, logger); break;
            }

            var result = uploader.Upload(file);

            //Save all Row if there is no valdiation error
            if (result.Valid)
            {
                transactionRepository.Save();
            }
            return result;
        }


        public IEnumerable<TransactionModel> GetTransactionsByCurrency(string currency)
        {
            return TransactionHelper.MapTransaction(transactionRepository.GetByCurrencyCode(currency).Result.ToList());
        }

        public IEnumerable<TransactionModel> GetTransactionsByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            return TransactionHelper.MapTransaction(transactionRepository.GetTransactionsByDateRange(dateFrom, dateTo).Result.ToList());
        }

        public IEnumerable<TransactionModel> GetTransactionsByStatus(string status)
        {
            TransactionStatus transactionStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), status);

            return TransactionHelper.MapTransaction(transactionRepository.GetByStatus((int)transactionStatus).Result.ToList());
        }

        public IEnumerable<TransactionModel> GetTransactionsAll()
        {
            return TransactionHelper.MapTransaction(transactionRepository.GetAll().Result.ToList());
        }

        
    }
}
