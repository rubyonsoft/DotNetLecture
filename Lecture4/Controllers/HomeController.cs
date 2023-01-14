using Microsoft.AspNetCore.Mvc;
using Northwind7.Models;
using Northwind7.Service;
using System.Diagnostics;

namespace Northwind7.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Register(Customer user)
        {
			if(!ModelState.IsValid)
			{
				return View(user);
			}

            CustomerService customerService = new CustomerService();
            ResultViewModel result = customerService.InsertCustomer(user);

            if (result.resultCode == 1)
                return View("login");
            else
            {
                ViewData["message"] = result.message;
                return View();
            }
        }

        public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Login(Customer user)
        {
            CustomerService customerService = new CustomerService();
            ResultViewModel result = customerService.LoginCustomer(user);

            if (result.resultCode == 1)
            {
                return View("Index");
            }
            else
            {
                ViewData["message"] = result.message;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}