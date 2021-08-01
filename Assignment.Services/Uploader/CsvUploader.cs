using Assignment.Repositories.Interface;
using Assignment.Services.Helpers;
using Assignment.Services.Interface;
using Assignment.Services.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Assignment.Services.Uploader
{
    public class CsvUploader : IUploader
    {

        private readonly ITransactionRepository transactionRepository;
        private readonly ILogger logger;

        private string ValidationError;

        public CsvUploader(ITransactionRepository transactionRepository, ILogger _logger)
        {
            this.transactionRepository = transactionRepository;
            logger = _logger;
        }

        public ResponseResult Upload(IFormFile file)
        {
            try
            {
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture);
                config.HasHeaderRecord = false;
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<TransactionModel>().ToList();

                    for (int i = 0; i < records.Count(); i++)
                    {
                        var row = records[i];
                        if (IsRecordValid(row, i))
                        {
                            TransactionHelper.SaveTransaction(row, transactionRepository);
                        }
                    }
                 
                }

                if (!String.IsNullOrEmpty(ValidationError))
                {
                    return ResponseResult.HasError(ValidationError);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while uploading CSV file");
                return ResponseResult.HasError(ex.Message);
            }

            return ResponseResult.Ok();
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
