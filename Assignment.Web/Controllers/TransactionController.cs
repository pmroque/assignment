using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assignment.Web.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return RedirectToAction("Index", "Transaction", new { message = "Select A file" });

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://localhost:58212/transaction/upload");

                    byte[] data;
                    using (var br = new BinaryReader(file.OpenReadStream()))
                        data = br.ReadBytes((int)file.OpenReadStream().Length);

                    ByteArrayContent bytes = new ByteArrayContent(data);


                    MultipartFormDataContent multiContent = new MultipartFormDataContent();

                    multiContent.Add(bytes, "file", file.FileName);

                    var response = client.PostAsync("upload", multiContent).Result;

                   var result = response.Content.ReadAsStringAsync().Result;


                    return RedirectToAction("Index", "Transaction", new { message = result});
                }
                catch (Exception)
                {
                    return StatusCode(500); // 500 is generic server error
                }
            }            
        }
    }
}
