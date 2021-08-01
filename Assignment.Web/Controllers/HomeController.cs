using Assignment.Models;
using Assignment.Repositories.Interface;
using Assignment.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ITransactionRepository transactionRepo;

        public HomeController(ILogger<HomeController> logger, ITransactionRepository _repository)
        {
            _logger = logger;
            transactionRepo = _repository;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58212/");
                //HTTP GET
                var responseTask = client.GetAsync("transaction/all");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var jsonString = result.Content.ReadAsStringAsync().Result;

                    var transactions = JsonSerializer.Deserialize<Transaction[]>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    var vmTransactions = new List<TransactionViewModel>();
                    foreach (var item in transactions)
                    {
                        vmTransactions.Add(new TransactionViewModel()
                        {
                            Id = item.TransactionId,
                            Payment = item.Amount + " " + item.CurrencyCode,
                            Status = item.StatusId.ToString()
                        });
                    }
                    model.Transactions = vmTransactions;
                }
            }

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
