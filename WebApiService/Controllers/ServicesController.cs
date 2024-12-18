﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebApiService.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService service;

        public ServicesController(IServicesService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route(nameof(ReadAll))]
        public IActionResult ReadAll()
        {
            ServiceModel[] services = service.GetAll();

            if (service != null) return Ok(services);
            else return StatusCode(500, new { Message = "Не удалось получить список услуг из БД!"});
        }

        [HttpGet]
        [Route("Read/{id:int}")]
        public IActionResult Read(int id)
        {
            return Ok(service.Get(id));
        }

        [HttpPost]
        [Route(nameof(Create))]
        [Authorize]
        public IActionResult Create(ServiceModel model)
        {
            service.Add(model);
            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        [Authorize]
        public IActionResult Update(ServiceModel model)
        {
            service.Update(model);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok();
        }
    }
}
