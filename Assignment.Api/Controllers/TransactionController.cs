using Assignment.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Assignment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private protected ITransactionService transaoctinService { get; set; }

        private string[] VALID_EXTENSIONS = { "csv", "xml", "png" };
        public TransactionController(ITransactionService service)
        {
            transaoctinService = service;
        }

        [HttpGet]
        [Route("currency/{currency}")]
        public IActionResult GetTransactionsByCurrency(string currency)
        {
            return Ok(transaoctinService.GetTransactionsByCurrency(currency));
        }

        [HttpGet]
        [Route("dateFrom/{dateFrom}/dateTo/{dateTo}")]
        public IActionResult GetTransaction(DateTime dateFrom, DateTime dateTo)
        {
            return Ok(transaoctinService.GetTransactionsByDateRange(dateFrom, dateTo));
        }

        [HttpGet]
        [Route("status/{status}")]
        public IActionResult GetTransactionsByStatus(string status)
        {
            return Ok(); // Ok(transaoctinService.GetTransactionsByStatus(status));
        }

        [HttpPost("upload", Name = "upload")]
        public IActionResult Upload(IFormFile file)
        {

            if(!IsValidFile(file.FileName))
            {
                return BadRequest("Unknown Format");
            }

            var response = transaoctinService.Upload(file);

            if (response.Valid)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }           
        }


        private bool IsValidFile(string fileName)
        {
            var extension = fileName.Split('.')[fileName.Split('.').Length - 1];

            return VALID_EXTENSIONS.Contains(extension);
        }



    }
}
