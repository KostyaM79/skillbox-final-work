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
                if (service.Add(model)) return Ok(new { Message = "Данные успешно добавлены." });
                else return StatusCode(500, new { Message = "При добавлении данных в БД произошла ошибка!" });
            }
            else return BadRequest(new { Message = "Получены некорректные данные!" });
        }

        [HttpGet]
        [Route(nameof(ReadAll))]
        public IActionResult ReadAll()
        {
            OrderFullDataModel[] data = service.GetAll();
            if (data != null) return Ok(data);
            else return StatusCode(500, new { Message = "Не удалось получить данные из БД." });
        }
    }
}
