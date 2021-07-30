using Assignment.Data.Models;
using Assignment.Repositories.Interface;
using Assignment.Services.Helpers;
using Assignment.Services.Interface;
using Assignment.Services.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class TransactionService : ITransactionService
    {
        private string[] VALID_EXTENSIONS = { "csv", "xml", "png" };
        private string currentExtension = "";

        private protected ITransactionRepository transactionRepository { get; set; }

        public TransactionService(ITransactionRepository _repository)
        {
            transactionRepository = _repository;
        }

        public HttpStatusCode Upload(IFormFile file)
        {

            //Check if the file is valid
            if (!CheckIfValidFile(file.FileName))
            {
                return HttpStatusCode.BadRequest;
            }

            if (currentExtension == "csv")
            {
                return ProcessCSVFile(file);
            }
            else
            {
                return ProcessXMLFile();
            }

        }

        private HttpStatusCode ProcessXMLFile()
        {
            throw new NotImplementedException();
        }

        private HttpStatusCode ProcessCSVFile(IFormFile file)
        {
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture);
            config.HasHeaderRecord = false;
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<TransactionRow>().ToList();

                for (int i = 0; i < records.Count(); i++)
                {
                    var row = records[i];
                    if (IsRecordValid(row, i))
                    {
                        SaveRow(row);
                    }
                }              

                transactionRepository.Save();
            }

            return HttpStatusCode.OK;
        }

        private void SaveRow(TransactionRow row)
        {
            var transaction = new Transaction()
            {
                TransactionId = row.TransactionId,
                Amount = decimal.Parse(row.Amount),
                CurrencyCode = row.Currency,
                TransactionDate = DateTime.Parse(row.TransactionDate),
                //StatusId = row.Status
            };
            transactionRepository.Add(transaction);
        }

        private bool IsRecordValid(TransactionRow item, int index)
        {
            var result = ValidationHelper.ValidateTransaction(item);

            if(string.IsNullOrEmpty(result))
            {
                return true;
            }

            //log the error
            result = $"Row {index+1} {result}";
            return false;
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
            currentExtension = fileName.Split('.')[fileName.Split('.').Length - 1];

            return VALID_EXTENSIONS.Contains(currentExtension);
        }

    }
}
