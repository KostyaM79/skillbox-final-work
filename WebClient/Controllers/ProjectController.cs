using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
