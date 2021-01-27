using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeCook.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    public class OrderManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}