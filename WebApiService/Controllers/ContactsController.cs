using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WebApiService.Services;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IApiSocialsService service;

        public ContactsController(IApiSocialsService service)
        {
            this.service = service;
        }


        [HttpGet]
        [Route(nameof(ReadAll))]
        public IActionResult ReadAll()
        {
            return Ok(service.GetAll());
        }


        [HttpPost]
        [Route(nameof(Update))]
        [Authorize]
        public void Update([FromForm] string[] links)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;
            service.Update(links, files);
        }
    }
}
