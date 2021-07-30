using Assignment.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Services.Helpers
{
    public class ValidationHelper
    {
        public static string ValidateTransaction(TransactionRow row)
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

            if(String.IsNullOrEmpty(row.Status))
            {
                errorMessage += "| Status is invalid";
            }

           

            return errorMessage;

        }
    }
}
