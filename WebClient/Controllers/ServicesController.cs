using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Services;

namespace WebClient.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServicesService service;

        public IActionResult ReadAll()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(ServiceModel model)
        {
            service.Add(model);
            return RedirectToAction("ReadAll");
        }
    }
}
