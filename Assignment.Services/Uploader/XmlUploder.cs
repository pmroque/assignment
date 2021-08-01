using Assignment.Repositories.Interface;
using Assignment.Services.Helpers;
using Assignment.Services.Interface;
using Assignment.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assignment.Services.Uploader
{
    public class XmlUploder : IUploader
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ILogger logger;
        public XmlUploder(ITransactionRepository transactionRepository, ILogger _logger)
        {
            this.transactionRepository = transactionRepository;
            logger = _logger;
        }

        private string ValidationError;

        public ResponseResult Upload(IFormFile file)
        {
            try
            {
                var reader = XmlReader.Create(file.OpenReadStream());
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TransactionsXML));
                var records = (TransactionsXML)serializer.Deserialize(reader);

                for (int i = 0; i < records.Transaction.Count(); i++)
                {
                    var row = MaptoTransactionModel(records.Transaction[i]);
                    if (IsRecordValid(row, i))
                    {
                        TransactionHelper.SaveTransaction(row, transactionRepository);
                    }
                }

                if (!String.IsNullOrEmpty(ValidationError))
                {
                    return ResponseResult.HasError(ValidationError);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while uploading XML file");
                return ResponseResult.HasError(ex.Message);
            }

            return ResponseResult.Ok();
        }

        private TransactionModel MaptoTransactionModel(TransactionXML transaction)
        {
            return new TransactionModel()
            {
                TransactionId = transaction.Id,
                Amount = transaction.PaymentDetails.Amount,
                Currency = transaction.PaymentDetails.CurrencyCode,
                Status = transaction.Status,
                TransactionDate = transaction.TransactionDate

            };
        }

        private bool IsRecordValid(TransactionModel item, int index)
        {
            var result = TransactionHelper.ValidateTransaction(item);
            if (string.IsNullOrEmpty(result))
            {
                return true;
            }

            //log the error
            result = $"Row {index + 1} {result}";
            ValidationError += result + "\n";
            logger.LogWarning(result);

            return false;
        }

    }
}
