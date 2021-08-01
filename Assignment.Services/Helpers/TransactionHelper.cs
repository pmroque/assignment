using Assignment.Data.Models;
using Assignment.Repositories.Interface;
using Assignment.Services.Models;
using System;
using System.Collections.Generic;

namespace Assignment.Services.Helpers
{
    public static class TransactionHelper
    {
        public static bool SaveTransaction(TransactionModel row, ITransactionRepository transactionRepository)
        {
            var transaction = new Transaction()
            {
                TransactionId = row.TransactionId,
                Amount = decimal.Parse(row.Amount),
                CurrencyCode = row.CurrencyCode,
                TransactionDate = DateTime.Parse(row.TransactionDate),
                StatusId = (int)(TransactionStatus)Enum.Parse(typeof(TransactionStatus), row.Status)
            };
            return transactionRepository.Add(transaction).Result;
        }
        public static string ValidateTransaction(TransactionModel row)
        {
            var errorMessage = "";

            if (String.IsNullOrEmpty(row.TransactionId))
            {
                errorMessage += "| Transaction Id is invalid";
            }

            if (!decimal.TryParse(row.Amount, out var output))
            {
                errorMessage += "| Amount is invalid";
            }

            if (!DateTime.TryParse(row.TransactionDate, out var outputDate))
            {
                errorMessage += "| Transaction Date is invalid";
            }

            if (String.IsNullOrEmpty(row.Status))
            {
                errorMessage += "| Status is invalid";
            }
            else
            {
                try
                {
                    TransactionStatus transactionStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), row.Status);
                }
                catch (Exception)
                {

                    errorMessage += "| Status is invalid";
                }
            }

            return errorMessage;

        }

        public static List<TransactionModel> MapTransaction(List<Transaction> transactions)
        {
            var transactionModelList = new List<TransactionModel>();

            transactions.ForEach(t =>
            {
                transactionModelList.Add(new TransactionModel()
                {
                    TransactionId = t.TransactionId,
                    Amount = t.Amount.ToString(),
                    CurrencyCode = t.CurrencyCode,
                    TransactionDate = t.TransactionDate.ToString(),
                    Status = t.Status?.Name,
                    OutputStatus = t.Status?.OutputStatus
                });
            });
            return transactionModelList;
        }
    }
}
