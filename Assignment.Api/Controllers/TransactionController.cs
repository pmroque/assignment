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
            return Ok(transaoctinService.GetTransactionsByStatus(status));
        }

        [HttpPost("upload", Name = "upload")]
        public IActionResult Upload(IFormFile file)
        {

            var statusCode = transaoctinService.Upload(file);
            if (statusCode == HttpStatusCode.OK)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }           
        }


    }
}
