using Assignment.Data.Models;
using Assignment.Repositories.Interface;
using Assignment.Services.Models;
using System;

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
                CurrencyCode = row.Currency,
                TransactionDate = DateTime.Parse(row.TransactionDate),
                //StatusId = row.Status
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

            return errorMessage;

        }
    }
}
