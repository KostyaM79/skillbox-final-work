using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Models;
using WebApiService.Services;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService service;

        public OrdersController(IOrderService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create([FromBody] OrderModel model)
        {
            if (ModelState.IsValid)
            {
                if (service.Add(model)) return Ok();
                else return StatusCode(500);
            }
            else return BadRequest();
        }
    }
}
