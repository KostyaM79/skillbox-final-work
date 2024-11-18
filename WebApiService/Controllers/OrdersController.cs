using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebApiService.Services;
using WebApiService.Data;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IApiOrderService service;

        public OrdersController(IApiOrderService service)
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
            OrdersListModel data = service.GetAll();
            if (data != null) return Ok(data);
            else return StatusCode(500, new { Message = "Не удалось получить список заявок из БД." });
        }

        [HttpGet]
        [Route("Read/{filterName}/startOffset/{startOffset}/endOffset/{endOffset}")]
        public IActionResult Read(string filterName, int startOffset, int endOffset)
        {
            OrderFilter filter = new OrderFilter(filterName, startOffset, endOffset);
            return Ok(service.Get(filter.Filter));
        }

        [HttpGet]
        [Route("read/{id}")]
        public IActionResult Read(int id)
        {
            ModifyOrderModel data = service.Get(id);
            if (data != null) return Ok(data);
            else return StatusCode(500, new { Message = "Не удалось получить заявку из БД." });
        }

        [HttpPost]
        [Route(nameof(Update))]
        public IActionResult Update(UpdateOrderModel model)
        {
            service.Update(model);
            return Ok();
        }
    }
}
