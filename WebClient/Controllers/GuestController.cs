using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult IndexGuest()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View();
        }

        public IActionResult Project()
        {
            return View();
        }
    }
}
