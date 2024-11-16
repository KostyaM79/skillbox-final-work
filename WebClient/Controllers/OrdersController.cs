using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebClient.Services;
using Exceptions;
using WebClient.Models;
using Services;

namespace WebClient.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IClientOrderService orderService;

        public OrdersController(IClientOrderService service)
        {
            orderService = service;
        }

        [HttpGet]
        public IActionResult Order()
        {
            return View();
        }

        public IActionResult ReadAll()
        {
            return View(orderService.GetAll());
        }

        [Route("read/filter/{filterName}/{startOffset?}/{endOffset?}")]
        public IActionResult Read(string filterName, int startOffset = 0, int endOffset = 0)
        {
            return View("ReadAll", orderService.Get(filterName, startOffset, endOffset));
        }

        [Route("read/{id:int}")]
        public IActionResult Find(int id)
        {
            return View(orderService.Get(id));
        }

        /// <summary>
        /// Если данные валидны, отправляет их в сервис для добавления в базу данныхю
        /// Затем перенаправляет пользователя на начальную странице
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Order(OrderModel model)
        {
            if (ModelState.IsValid) orderService.Add(model);

            return RedirectToAction("Main", "SiteApp");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Update(int id)
        {
            return View(orderService.Get(id));
        }

        [HttpPost]
        public IActionResult Update(UpdateOrderModel model)
        {
            try
            {
                orderService.Update(model);
                return Redirect("ReadAll");
            }
            catch (DatabaseServiceException ex)
            {
                return View("DbError", new ErrorViewModel() { ErrorMessage = $"При обращении к сервису базы данных произошла ошибка: {ex.Message}" });
            }
        }
    }
}
