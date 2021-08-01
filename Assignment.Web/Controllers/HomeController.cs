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
        private const string UriString = "http://localhost:58212/";
        private readonly ILogger<HomeController> _logger;

        private ITransactionRepository transactionRepo;

        public HomeController(ILogger<HomeController> logger, ITransactionRepository _repository)
        {
            _logger = logger;
            transactionRepo = _repository;
        }

        public IActionResult Index(string searchBy, string searchString, string dateFrom, string dateTo)
        {          
           
           
           

            var model = new HomeViewModel();

            switch (searchBy)
            {
                case "STATUS": model.Transactions = SearchByStatus(searchString);
                    @ViewData["searchStringStatus"] = searchString;
                    break;
                case "CURRENCY": model.Transactions = SearchByCurrency(searchString);
                    @ViewData["searchStringCurrency"] = searchString;
                    break;
                case "DATE": model.Transactions = SearchByDate(dateFrom, dateTo);
                    @ViewData["searchDateFrom"] = dateFrom;
                    @ViewData["searchDateTo"] = dateTo; 
                    break;
                default: model.Transactions = GetAll(); break;
            }          

            return View(model);
        }


        private List<TransactionViewModel>  GetAll()
        {
            var url = "api/transaction/all";
            return GetTransactionRequest(url);
        }

        private List<TransactionViewModel> SearchByStatus( string status)
        {
            var url = "api/transaction/status/" + status;
            return GetTransactionRequest(url);
        }

        private List<TransactionViewModel> SearchByCurrency(string currency)
        {
            var url = "api/transaction/currency/" + currency;
            return GetTransactionRequest(url);
        }

        private List<TransactionViewModel> SearchByDate(string dateFrom, string dateTo)
        {
            var url = $"api/transaction/dateFrom/{dateFrom}/dateTo/{dateTo}";
            return GetTransactionRequest(url);
        }

        private static List<TransactionViewModel> GetTransactionRequest(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UriString);
                //HTTP GET
                var result = client.GetAsync(url).Result;

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
                            Status = item.OutputStatus
                        });
                    }
                    return vmTransactions;
                }

                return null;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
