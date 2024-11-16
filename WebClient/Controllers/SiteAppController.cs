using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using WebClient.Services;
using Models;
using Services;

namespace WebClient.Controllers
{
    public class SiteAppController : Controller
    {
        private readonly IPhrasesService service;

        public SiteAppController(IPhrasesService service)
        {
            this.service = service;
        }

        public IActionResult Main()
        {
            return View(new PhraseModel() { Phrase = service.GetPhrase() });
        }

        public IActionResult Projects()
        {
            return View();
        }
    }
}
