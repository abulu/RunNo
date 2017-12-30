using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunNo_MVC.Models;

namespace RunNo_MVC.Controllers
{
    public class HomeController : Controller
    {
        //int id
        public IActionResult Index(string Id)
        {
            // ViewData["Number"] =    

            //     return View();

            // User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299
            //var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault();

            string ip = Request.Headers["User-Agent"].FirstOrDefault();
            string iKey = Id != null && Id.Length > 0 ? Id : NumberObject.GenerateMD5(ip);

            Product myProduct = new Product
            {
                GUID = iKey,
                Name = NumberObject.PickNumber(iKey)
            };


            return View(myProduct);

        }


        public IActionResult About(int Id)
        {
            NumberObject.SetLimitNo(Id.ToString());

            Product myProduct = new Product
            {
                GUID = "SettingNo",
                Name = Id.ToString()
            };

            return View(myProduct);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
