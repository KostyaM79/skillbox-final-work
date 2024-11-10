using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService service)
        {
            orderService = service;
        }

        [HttpGet]
        public IActionResult Order()
        {
            return View();
        }

        /// <summary>
        /// Инициирует добавление новой заявки пользователя в сервисе OrderService.
        /// При удачном добавлении заявки, перенаправляет пользователя на начал ную страницу.
        /// При неудаче возвращает ошибку.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Order(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                if (orderService.Add(model))
                    return Redirect("/Guest/IndexGuest");
                else
                    return Problem(detail: "Не удалось добавить заявку!");
            }
            else
                return Problem(detail: "Не все поля заполнены!");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult EditOrder(int id)
        {
            return View(orderService.Get(id));
        }

        [HttpPost]
        public IActionResult Update(UpdateOrderModel model)
        {
            if (orderService.UpdateOrder(model)) return Redirect("/Admin/AdminDesktop");
            else return Problem("Не удалось обновить статус заявки!");
        }
    }
}
