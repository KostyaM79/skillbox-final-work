using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IClientSocialsService service;

        public ContactsController(IClientSocialsService service)
        {
            this.service = service;
        }

        public IActionResult Read()
        {
            return View(service.GetAll());
        }

        [Authorize]
        public IActionResult Update(string[] links)
        {
            string token = HttpContext.Request.Cookies["jwt"];
            IFormFileCollection files = HttpContext.Request.Form.Files;
            service.Update(links, files, token);
            return RedirectToAction("Read", "Contacts");
        }
    }
}