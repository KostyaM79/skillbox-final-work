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
            else return StatusCode(500, new { Message = "Не удалось получить список заявок из БД." });
        }

        [HttpGet]
        [Route("Read/{filterName}/startOffset/{startOffset}/endOffset/{endOffset}")]
        public IActionResult Read(string filterName, int startOffset, int endOffset)
        {
            switch (filterName)
            {
                case "Today": return Ok(service.Get((Order e) => e.CreatingDate.Date == DateTime.Today));
                case "Yesterday": return Ok(service.Get((Order e) => e.CreatingDate.Date == DateTime.Today.AddDays(-1)));
                case "Week": return Ok(service.Get((Order e) => e.CreatingDate.Date >= DateTime.Today.AddDays(-6) && e.CreatingDate.Date <= DateTime.Today));
                case "Month": return Ok(service.Get((Order e) => e.CreatingDate.Date > DateTime.Today.AddMonths(-1) && e.CreatingDate.Date <= DateTime.Today));
                case "Range": return Ok(service.Get((Order e) => e.CreatingDate.Date >= DateTime.Today.AddDays(-startOffset) && e.CreatingDate.Date <= DateTime.Today.AddDays(-endOffset)));
            }
            return Problem();
        }

        [HttpGet]
        [Route(nameof(ReadByDate))]
        public IActionResult ReadByDate()
        {
            OrderFullDataModel[] data = service.GetByDate(DateTime.Now);
            if (data != null) return Ok(data);
            else return StatusCode(500, new { Message = "Не удалось получить список заявок из БД." });
        }

        [HttpGet]
        [Route(nameof(ReadByYesterday))]
        public IActionResult ReadByYesterday()
        {
            DateTime date = DateTime.Now.AddDays(-1);
            OrderFullDataModel[] data = service.GetByDate(date);
            if (data != null) return Ok(data);
            else return StatusCode(500, new { Message = "Не удалось получить список заявок из БД." });
        }

        [HttpGet]
        [Route(nameof(ReadByWeek))]
        public IActionResult ReadByWeek()
        {
            OrderFullDataModel[] data = service.GetByWeek();
            if (data != null) return Ok(data);
            else return StatusCode(500, new { Message = "Не удалось получить список заявок из БД." });
        }

        [HttpGet]
        [Route("read/{id}")]
        public IActionResult Read(int id)
        {
            ModifyOrderModel data = service.GetOrder(id);
            if (data != null) return Ok(data);
            else return StatusCode(500, new { Message = "Не удалось получить заявку из БД." });
        }

        [HttpPost]
        [Route(nameof(Update))]
        public IActionResult Update(UpdateOrderModel model)
        {
            if (service.Update(model)) return Ok();
            else return StatusCode(500, new { Message = "Не удалось обновить статус заявки!" });
        }
    }
}
