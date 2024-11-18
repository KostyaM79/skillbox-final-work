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

        public ServicesController(IServicesService service)
        {
            this.service = service;
        }

        public IActionResult ReadAll()
        {
            return View(service.GetAll());
        }

        public IActionResult Read(int id)
        {
            return View(service.Get(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceModel model)
        {
            service.Add(model);
            return Redirect("/Services/ReadAll");
        }

        public IActionResult Update(ServiceModel model)
        {
            service.Update(model);
            return Redirect("/Services/ReadAll");
        }

        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Redirect("/Services/ReadAll");
        }
    }
}
